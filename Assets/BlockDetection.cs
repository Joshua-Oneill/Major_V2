using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetection : MonoBehaviour
{


    public CubeSpawn cubeSpawnScript;
    public tileGenerator tileGeneratorScript;

    RaycastHit hit = new RaycastHit(); //sets up the ability to receive information when a Ray hits a collider

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
       
        //Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); //creates a ray based on the position of the  mouse through the player Camera
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 10, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10))
            {
                Debug.Log(hit.collider.name);
                //hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
               
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

                Debug.Log($"x: {xIn} y: {yIn} z: {zIn}");
                Debug.Log("Chunk X = " + xChunk + "Chunk Y =" + yChunk);
                Debug.Log(hit.transform.position);


                GameObject go = new GameObject();
                go.transform.position = new Vector3(0, 0, 0);

                tileGeneratorScript.chunkArray[xChunk, yChunk][xIn, yIn, zIn] = go;

                if (hit.collider.name == "rightFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3((xIn+1) + (xChunk * 10), yIn, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(3, 16), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn + 1, yIn, zIn);
                }
                if (hit.collider.name == "leftFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3((xIn - 1) + (xChunk * 10), yIn, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(3, 16), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn - 1, yIn, zIn);
                }
                if (hit.collider.name == "topFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3(xIn  + (xChunk * 10), yIn + 1, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(3, 16), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn, yIn + 1, zIn);
                }
                if (hit.collider.name == "frontFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3(xIn + (xChunk * 10), yIn, (zIn  + (yChunk * 10)) + 1), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(3, 16), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn, yIn, zIn + 1);
                }
                if (hit.collider.name == "backFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3(xIn + (xChunk * 10), yIn, (zIn - 1) + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(3, 16), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn, yIn, zIn -1);
                }

                if (hit.collider.name == "bottomFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3(xIn + (xChunk * 10), yIn - 1, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(3, 16), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn, yIn - 1, zIn);
                }


               // CubeGenerator.CreateCube(go, new Vector3(xIn + (xChunk * 10) , yIn, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(3, 16), cubeSpawnScript.neighbours);
                
            }
        }

    }

    public void BlockPlacement() 
    { 
        
    
    }

    public Sides CalculateNeighbours(GameObject go, GameObject[,,] cubeArray)
    {
        Sides cubeSide = new Sides();

        cubeSide.top = false;
        cubeSide.front = false;
        cubeSide.bottom = false;
        cubeSide.back = false;
        cubeSide.left = false;
        cubeSide.right = false;

        if(((int)go.transform.position.y + 1) > cubeArray.GetLength(1) && (cubeArray[(int)go.transform.position.x, (int)go.transform.position.y + 1, (int)go.transform.position.z] != null)) 
        {
            cubeSide.bottom = true;
            
        }
        if (((int)go.transform.position.y - 1) > 0 && (cubeArray[(int)go.transform.position.x, (int)go.transform.position.y - 1, (int)go.transform.position.z] == null))
        {
            cubeSide.top = true;
        }
        if (((int)go.transform.position.x + 1) > cubeArray.GetLength(0) && cubeArray[(int)go.transform.position.x + 1, (int)go.transform.position.y, (int)go.transform.position.z] == null)
        {
            cubeSide.right = true;
        }
        if (((int)go.transform.position.x - 1) > 0 && cubeArray[(int)go.transform.position.x - 1, (int)go.transform.position.y, (int)go.transform.position.z] == null)
        {
            cubeSide.left = true;
        }
        if (((int)go.transform.position.z + 1) > cubeArray.GetLength(2) && cubeArray[(int)go.transform.position.x, (int)go.transform.position.y, (int)go.transform.position.z + 1] == null)
        {
            cubeSide.front = true;
        }
        if (((int)go.transform.position.z - 1) > 0 && cubeArray[(int)go.transform.position.x, (int)go.transform.position.y, (int)go.transform.position.z - 1] == null)
        {
            cubeSide.back = true;
        }





        return cubeSide;
    }

}
