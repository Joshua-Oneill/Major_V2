using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//this script is responsible for creating each cube by refrencing the CUbeGenerator script and passing in the requires coordinate offset 
//in thi way we can create all the cubes, we also utilise the nposemap here in order to create empty space on the map that replicates the noise texture
//essentially this entire script creates the unique terrain and is able to place the blocks in the correct specified location. we also add and remove blocks by using this script
//aswell as determining whether a block will have a neighbour around it which passes that information in the form of a bool value to the CubeGenerator script whihc runs through the if statment cluster,
//and detemrines if the face should be created or not

[System.Serializable]
public class TerrainType
{
    public string name;
    public float height;
    public Vector2 coords;   

    public TerrainType(string n, float h, Vector2 c)
    {
        name = n;
        height = h;
        coords = c;
    }
}

public struct Sides
{
    public bool top;
    public bool bottom;
    public bool back;
    public bool front;
    public bool left;
    public bool right;

}

public class CubeSpawn : MonoBehaviour
{
    //defining the aspects of the TerrainType struct whihc will be used to determine whihc texture will belong to each,
    //cube depending on their height in the heihgt map, for exmapke all heihg values above 0.6 will be grass,
    //the vector tells the CubeGenrator script the exact offset for the textureAtlas to achieve the desired zoomed in texture on the face 
    private TerrainType[] terrainTypes =
    {
        new TerrainType("grassTop", 0.6f, new Vector2(2, 7)),
        new TerrainType("grass", 0.5f, new Vector2(3, 16)),
        new TerrainType("water", 0.4125f, new Vector2(15, 4)),
        new TerrainType("stone", 0.4f, new Vector2(2, 16)),
        new TerrainType("diamond", 0.3f, new Vector2(3, 13))
            
    };




    public GameObject[,,] cubeArray; //3 dimensional array that holds all the cubes within it
    public Material textureAtlas;

    //this Gameobject array is returned to tileGenerator with all the values for the cubes to be stored in the ChunkArray, aswell as tellingthe CubeGenerator script where to offset the cubes
    public GameObject[,,] CubeMake(int length, int depth, int height, float[,] heightMap, int chunkX, int chunkY)
    {
        //paramters are obtained from the tileGenrator script and they specift the exact size of the chunk, then the script runs through each value in the nested loops to check 
        //with the heightmap values whether a cube should be spawned in that x,y,z coordinate or not

        cubeArray = new GameObject[length , height, depth ];
        for (int xIndex = 0; xIndex < length; xIndex++)
        {
            for (int zIndex = 0; zIndex < depth; zIndex++)
            {
                for (int yIndex = 0; yIndex < height; yIndex++)
                {
                    int xOffset = chunkX * length;
                    int zOffset = chunkY * depth;
                    
                    // height map is 0-1 also divided by 2.0 as a seondary map scale a heigher number will be smoother a lower number is more tall and rigid,
                    // y/height is in decimal form which will be 0-1. 
                    //if the heighmap values at the zIndex and yIndex postions are greater then the yIndex devided by the total height specified example 4/10 will be 0.4
                    //then the perlin noise values will be in float form for example 0.5, then it will spawn a cube here
                    if (heightMap[xIndex + xOffset, zIndex + zOffset] /2.0 > yIndex/(double)height )
                    {
                        //runs the funtion to infrom the cubeGenerator script if there are neihgbors to the sides or not
                        Sides neighbours = CalculateNeighbours(heightMap, new Vector3(xIndex + xOffset, yIndex/(float)height , zIndex + zOffset));

                        //each cube will no be made a gameobject with this line we creat a gameobject then late rits assigned to a cube 
                        GameObject go = new GameObject();
                        
                        //gameobject that was just made is added to the index of the aray wherever the nested loops are currently at
                        //so if this statment doesnt run form the if statment it will be simply empty space with nothing in it but the array is intialized to be a certain size so thats how we can add blocks into the map and onto the array
                        cubeArray[xIndex, yIndex, zIndex] = go;

                        //sending all parameters to the CubeGeneator script norde rto initialize a cube in the world

                        CubeGenerator.CreateCube(go, new Vector3(0, 0, 0), textureAtlas, 
                            ChooseTerrainType(heightMap[xIndex + xOffset, zIndex + zOffset]), neighbours);

                        //these sections allow each face to have a coordinate position and lets us set up a naming convention to indicate the chunk and x,y,z place of each cube within the unity hierachy 
                        go.transform.position = new Vector3(xIndex + xOffset, yIndex, zIndex + zOffset);

                        go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", chunkX, chunkY, xIndex , yIndex, zIndex);
                        
                       
                    }
                }
            }
        }

        return cubeArray;
    }

   

    public Sides CalculateNeighbours(float [,] heightMap, Vector3 position)
    {
        //we create a new Sides variable from the Sides bool struct which contains a defintion for all 6 sides of a cube 
        Sides cubeSide = new Sides();

        //initially all bools in the struct are set to false so they all will spawn at this stage 
        cubeSide.top = false;
        cubeSide.front = false;
        cubeSide.bottom = false;
        cubeSide.back = false;
        cubeSide.left = false;
        cubeSide.right = false;

        //in this if statment cluster we runb the same stlye of if statment to in the CubeMake function, in heightmap / the y postion or height
        //by doing this we can check the perlin noise value adjacent to the current one we are looking at to see if there would be a cube next to it
        //we just manipulate all the x,y,z values by +1 or -1 to see if the the next spot in the perlin map, then if the heihgmap vlaue is larger then the height value next to it
        //then we wont make a cube face there (depending on where we are looking) 

        if (heightMap[(int)position.x, (int)position.z] / 2.0 > position.y + 0.1 ) //this one looks to seee if there is a cube above it, if there is then we dont spawn in the top face
        {
            cubeSide.top = true;
        }
        if (heightMap[(int)position.x, (int)position.z] / 2.0 > position.y - 0.1)
        {
            cubeSide.bottom = true;
        }
        if (position.x < heightMap.GetLength(0) - 1 && heightMap[(int)position.x + 1, (int)position.z] / 2.0 > position.y)//this one looks to the right of a face etc etc
        {
            cubeSide.right = true;
        }
        if (position.x > 0 && heightMap[(int)position.x - 1, (int)position.z] / 2.0 > position.y)
        {
            cubeSide.left = true;
        }
        if (position.z > 0 && heightMap[(int)position.x , (int)position.z - 1] / 2.0 > position.y)
        {
            cubeSide.front = true;
        }
        if (position.z < heightMap.GetLength(1)  -1 && heightMap[(int)position.x, (int)position.z + 1] / 2.0 > position.y)
        {
            cubeSide.back = true;
        }

        return cubeSide;
    }

    //selects what texture we create
    public Vector2 ChooseTerrainType(float height)
    {
        //for all the terrains we defiend above, if the current hiehgt value is larger then the specified heigh value of that texture then retunr its coords for the texture atlas back to where this function is being called 
        foreach (TerrainType terrainType in terrainTypes)
        {
            if (height > terrainType.height)
            {
                return terrainType.coords;
            }
        }
        return new Vector2(3, 16);
    }

   

}
