using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    

    private TerrainType[] terrainTypes =
    {
        new TerrainType("grassTop", 0.6f, new Vector2(2, 7)),
        new TerrainType("grass", 0.5f, new Vector2(3, 16)),
        new TerrainType("water", 0.4125f, new Vector2(15, 4)),
        new TerrainType("stone", 0.4f, new Vector2(2, 16)),
        new TerrainType("diamond", 0.3f, new Vector2(3, 13))
            
    };

    public Sides neighbours;


    public GameObject[,,] cubeArray;
    public Material textureAtlas;
    public GameObject[,,] CubeMake(int length, int depth, int height, float[,] heightMap, int chunkX, int chunkY)
    {
        

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
                    // y/height is in decimal form whihc will be 0-1. 
                    if (heightMap[xIndex + xOffset, zIndex + zOffset] /2.0 > yIndex/(double)height )
                    {
                        Sides neighbours = CalculateNeighbours(heightMap, new Vector3(xIndex + xOffset, yIndex/(float)height , zIndex + zOffset));

                        GameObject go = new GameObject();
                        
                        cubeArray[xIndex, yIndex, zIndex] = go;
                        CubeGenerator.CreateCube(go, new Vector3(0, 0, 0), textureAtlas, 
                            ChooseTerrainType(heightMap[xIndex + xOffset, zIndex + zOffset]), neighbours);

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
        Sides cubeSide = new Sides();

        cubeSide.top = false;
        cubeSide.front = false;
        cubeSide.bottom = false;
        cubeSide.back = false;
        cubeSide.left = false;
        cubeSide.right = false;

        Debug.Log(position);

        if (heightMap[(int)position.x, (int)position.z] / 2.0 > position.y + 0.1 )
        {
            cubeSide.top = true;
        }
        if (heightMap[(int)position.x, (int)position.z] / 2.0 > position.y - 0.1)
        {
            cubeSide.bottom = true;
        }
        if (position.x < heightMap.GetLength(0) - 1 && heightMap[(int)position.x + 1, (int)position.z] / 2.0 > position.y)
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


    public Vector2 ChooseTerrainType(float height)
    {
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
