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
        new TerrainType("grass", 0.5f, new Vector2(3,16)),
        new TerrainType("stone", 0.4f, new Vector2(2, 16)),
        new TerrainType("diamond", 0.3f, new Vector2(3, 13))
            
    };


    public GameObject[,,] cubeArray;
    public Material textureAtlas;
    public void CubeMake(int length, int depth, int height, float[,] heightMap, int chunkX, int chunkY)
    { 
        cubeArray = new GameObject[length + chunkX, height, depth + chunkY];
        for (int xIndex = 0; xIndex < length; xIndex++)
        {
            for (int zIndex = 0; zIndex < depth; zIndex++)
            {
                for (int yIndex = 0; yIndex < height; yIndex++)
                {
                    int xOffset = chunkX * length;
                    int yOffset = chunkY * depth;
                    
                    // height map is 0-1 also divided by 2.0 as a seondary map scale a heigher number will be smoother a lower number is more tall and rigid,
                    // y/height is in decimal form whihc will be 0-1. 
                    if (heightMap[xIndex + xOffset, zIndex + yOffset] /2.0 > yIndex/(double)height )
                    {
                        Sides neighbours = CalculateNeighbours(heightMap[xIndex + xOffset, zIndex + yOffset], new Vector3(xIndex + xOffset, yIndex, zIndex + yOffset));

                        GameObject go = new GameObject();
                        cubeArray[xIndex, yIndex, zIndex] = go;
                        CubeGenerator.CreateCube(go, new Vector3(xIndex + xOffset, yIndex, zIndex + yOffset), textureAtlas, 
                            ChooseTerrainType(heightMap[xIndex + xOffset, zIndex + yOffset]), neighbours);
                    }
                }
            }
        }
    }

   

    public Sides CalculateNeighbours(float heightMap, Vector3 position)
    {
        Sides cubeSide = new Sides();

        cubeSide.front = false;
        cubeSide.bottom = false;
        cubeSide.back = false;
        cubeSide.top = false;
        cubeSide.left = false;
        cubeSide.right = false;
        
        //if(heightMap/ 2.0 > position.y + 1)
        //{
        //    cubeSide.top = true;         
        //}
        //if (heightMap / 2.0 > position.y - 1)
        //{
        //    cubeSide.bottom = true;
        //}
        //if (heightMap / 2.0 > position.x + 1)
        //{
        //    cubeSide.right = true;
        //}
        //if (heightMap / 2.0 > position.x - 1)
        //{
        //    cubeSide.left = true;
        //}
        //if (heightMap / 2.0 > position.z + 1)
        //{
        //    cubeSide.back = true;
        //}
        //if (heightMap / 2.0 > position.z - 1)
        //{
        //    cubeSide.front = true;
        //}

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
