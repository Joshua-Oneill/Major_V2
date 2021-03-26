using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGeneration : MonoBehaviour
{
    public float [,] GenerateNoiseMap(int mapWidth, int mapDepth, float scale)
    {
        float[,] noiseMap = new float[mapDepth, mapWidth]; //creates an empty map of coordinates

        float offset = Random.Range(0, 1000);
        



        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapDepth; j++)
            {
                float sampleX = i / scale;
                float sampleZ = j / scale;
                //adjusting each map coordinate to the scale that was passed in

                float noise = Mathf.PerlinNoise(sampleX + offset, sampleZ + offset); //generates a noise map according to the x,z coordinates
                noiseMap[j, i] = noise;
            }

        }

        return noiseMap;
    }
}
