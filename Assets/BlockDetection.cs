﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetection : MonoBehaviour
{


    public CubeSpawn cubeSpawnScript;
    public tileGenerator tileGeneratorScript;

    public Camera playerCamera;
    //public CubeSpawn cubeSpawnScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RayDetect();
    }

    public void RayDetect()
    {
        RaycastHit hit = new RaycastHit(); //sets up the ability to receive information when a Ray hits a collider
        //Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); //creates a ray based on the position of the  mouse through the player Camera
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 10, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10))
            {
                Debug.Log(hit.collider.name);
                //hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                Debug.Log("Chunk X = " + hit.transform.parent.name.Substring(8,2));
                int xIn;
                int.TryParse(hit.transform.parent.name.Substring(24, 2), out xIn);
                int yIn;
                int.TryParse(hit.transform.parent.name.Substring(28, 2), out yIn);
                int zIn;
                int.TryParse(hit.transform.parent.name.Substring(32, 2), out zIn);

                int xChunk;
                int.TryParse(hit.transform.parent.name.Substring(8, 2), out xChunk);
                int yChunk;
                int.TryParse(hit.transform.parent.name.Substring(13, 2), out yChunk);

                GameObject go = new GameObject();
                tileGeneratorScript.chunkArray[xChunk, yChunk][xIn, yIn, zIn] = go;
                CubeGenerator.CreateCube(go, new Vector3(xIn, yIn, xIn), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(3, 16), cubeSpawnScript.neighbours);
                
            }
        }

    }

}
