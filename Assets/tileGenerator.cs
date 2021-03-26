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
   // public RawImage perlinImage;

    public int chunkSize; //the size of the entire map, square, number of chunks that make the map


    // Start is called before the first frame update
    void Start()
    {
        GenerateTile();
    }

    void GenerateTile()
    {
        //Vector3[] meshVertices = meshFilter.mesh.vertices;
        //int tileDepth = (int)Mathf.Sqrt(meshVertices.Length);
        //int tileWidth = tileDepth;

        float[,] heightMap = noiseGeneration.GenerateNoiseMap(tileLength * chunkSize, tileDepth * chunkSize, mapScale);

        for (int chunkX = 0; chunkX < chunkSize; chunkX++)
        {
            for (int chunkY = 0; chunkY < chunkSize; chunkY++)
            {
                cubeSpawnScript.CubeMake(tileLength, tileDepth, mapHeight, heightMap, chunkX, chunkY);

            }
        }





       
        //Texture2D tileTexture = BuildTexture(heightMap);
       // tileRenderer.material.mainTexture = tileTexture;
        
    }

    Texture2D BuildTexture(float[,] heightMap)
    {
        int tileWidth = heightMap.GetLength(0);
        int tileDepth = heightMap.GetLength(1);

        Color[] colorMap = new Color[tileDepth * tileWidth];

        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                int colorIndex = zIndex * tileWidth + xIndex;
                float height = heightMap[zIndex, xIndex];


                ////choosing a terrain type depending on the height value 
                //TerrainType terrainType = ChooseTerrainType(height);
                //colorMap[colorIndex] = terrainType.color;
                colorMap[colorIndex] = Color.Lerp(Color.black, Color.white, height);

            }
        }

        Texture2D tileTexture = new Texture2D(tileWidth, tileDepth);
        tileTexture.wrapMode = TextureWrapMode.Clamp;
        tileTexture.SetPixels(colorMap);
        tileTexture.Apply();
       // perlinImage.texture = tileTexture;

        return tileTexture;
    }

   
}
