using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//https://gamedevacademy.org/complete-guide-to-procedural-level-generation-in-unity-part-1/




public class tileGenerator : MonoBehaviour
{
    [SerializeField]
    NoiseGeneration noiseGeneration;

    [SerializeField]
    private MeshRenderer tileRenderer;

    [SerializeField]
    private MeshFilter meshFilter;

    [SerializeField]
    private MeshCollider meshCollider;

    [SerializeField]
    private float mapScale;

    [SerializeField]
    private int mapHeight;

    [SerializeField]
    CubeSpawn cubeSpawnScript;

    public int tileDepth;
    public int tileLength;

    public GameObject[,][,,] chunkArray;


    public int chunkNumber; //the number of chunks that will be instantied in order to create a larger combined terrain 

    public float[,] heightMap; //this array will be the perlin noise array 

    void Start()
    {
        GenerateTile(); //on the start of the program we generate one large tile or map, made up of the specified number of chunks 
    }

    void GenerateTile()
    {
        //assigning the value from the noismap script to the heighMap array, aswell as passing in the erquired data for the paramneters on the noiseGeneration script
        heightMap = noiseGeneration.GenerateNoiseMap(tileLength * chunkNumber, tileDepth * chunkNumber, mapScale); 

        //this array stores the position of every single cube that is created, the second 3 dimnesional array holds all the cubes within it, so the array knows whihc cube belongs to which chunk in the world
        chunkArray = new GameObject[chunkNumber, chunkNumber][,,];

        //this loop will run for the legnth of chunkSize, so if two chunks ae wanted we will call the cubemake script two times 
        for (int chunkX = 0; chunkX < chunkNumber; chunkX++)
        {
            for (int chunkY = 0; chunkY < chunkNumber; chunkY++)
            {
                //now we assign each spot of chunkArray at the position of the chunk we are looking at, say chunkX & chunkY are at 1&1 we will be working on the top right hand corner of the chunk, 
                //essentially two chunks are split into almos four total sections, one chunk has two parts to it. also pass in paramteres for the CubeMake script  
                chunkArray[chunkX, chunkY] = cubeSpawnScript.CubeMake(tileLength, tileDepth, mapHeight, heightMap, chunkX, chunkY);
            }
        }

    }

    //Texture2D BuildTexture(float[,] heightMap)
    //{
    //    int tileWidth = heightMap.GetLength(0);
    //    int tileDepth = heightMap.GetLength(1);

    //    Color[] colorMap = new Color[tileDepth * tileWidth];

    //    for (int zIndex = 0; zIndex < tileDepth; zIndex++)
    //    {
    //        for (int xIndex = 0; xIndex < tileWidth; xIndex++)
    //        {
    //            int colorIndex = zIndex * tileWidth + xIndex;
    //            float height = heightMap[zIndex, xIndex];


    //            ////choosing a terrain type depending on the height value 
    //            //TerrainType terrainType = ChooseTerrainType(height);
    //            //colorMap[colorIndex] = terrainType.color;
    //            colorMap[colorIndex] = Color.Lerp(Color.black, Color.white, height);

    //        }
    //    }

    //    Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
    //    tileTexture.wrapMode = TextureWrapMode.Clamp;
    //    tileTexture.SetPixels(colorMap);
    //    tileTexture.Apply();
    //   // perlinImage.texture = tileTexture;

    //    return tileTexture;
    //}

   
}
