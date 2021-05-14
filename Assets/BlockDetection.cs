using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetection : MonoBehaviour
{


    public CubeSpawn cubeSpawnScript;
    public tileGenerator tileGeneratorScript;


    public float textureX = 3f;
    public float textureY = 16f;
    

    RaycastHit hit = new RaycastHit(); //sets up the ability to receive information when a Ray hits a collider

    public Camera playerCamera;
  
    void Update()
    {
        RayDetect();
    }

    public void RayDetect()
    {
       
        //creates a ray based on the position of the player Camera
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 10, Color.green);
        
        //adding a block on left click 
        if (Input.GetMouseButtonDown(0))
        {   //sends out a ray in direction of player
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10))
            {
                Debug.Log(hit.collider.name);
                //hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
               

                //this bunch of TryParse's read off the postion of the long string name of each cube in the hierachy 
                //we can access the number for the chunk its in as well as the x,y,z spot of each cube 
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

                //if we hit a ibject with the raycast we create a new gamobject which will have faces added to it
                GameObject go = new GameObject();
                go.transform.position = new Vector3(0, 0, 0);

                //we ge the chunkarray from tileGenerator script and add the gamobject to to the spot the collider hit 
                tileGeneratorScript.chunkArray[xChunk, yChunk][xIn, yIn, zIn] = go;

                Debug.Log("this is before if: " + textureX +" " + textureY);

                //the if clusters identifys which face we hit with the ray and then we run the createcube function from cubeGenerator script to instatiate and entire new cube ajacent to the face we hit
                // on this first if statment if we hit the right face then we add a block on the xindex plus one or to the right of the hit object. we parse in all necesary paramters to make a cube and 
                //we run a sperate neighbour check function that is on this script instead
                if (hit.collider.name == "rightFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3((xIn+1) + (xChunk * 10), yIn, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(textureX, textureY), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn + 1, yIn, zIn);
                    Debug.Log("this is after if: " + textureX + " " + textureY);
                }
                if (hit.collider.name == "leftFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3((xIn - 1) + (xChunk * 10), yIn, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(textureX, textureY), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn - 1, yIn, zIn);
                }
                if (hit.collider.name == "topFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3(xIn  + (xChunk * 10), yIn + 1, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(textureX, textureY), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn, yIn + 1, zIn);
                    Debug.Log("this is after if: " + textureX + " " + textureY);
                }
                if (hit.collider.name == "frontFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3(xIn + (xChunk * 10), yIn, (zIn  + (yChunk * 10)) + 1), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(textureX, textureY), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn, yIn, zIn + 1);
                }
                if (hit.collider.name == "backFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3(xIn + (xChunk * 10), yIn, (zIn - 1) + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(textureX, textureY), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn, yIn, zIn -1);
                }

                if (hit.collider.name == "bottomFace")
                {
                    CubeGenerator.CreateCube(go, new Vector3(xIn + (xChunk * 10), yIn - 1, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material, new Vector2(textureX, textureY), CalculateNeighbours(go, cubeSpawnScript.cubeArray));
                    go.name = string.Format("Chunk(x:{0:D2} y:{1:D2})Index(x:{2:D2}y:{3:D2}z:{4:D2})", xChunk, yChunk, xIn, yIn - 1, zIn);
                }
                
            }
        }
        //removing a block on right click 
        else if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10))
            {
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


                CubeGenerator.CreateCube(tileGeneratorScript.chunkArray[xChunk, yChunk][(xIn - 1), yIn, zIn], new Vector3((xIn -1) + (xChunk * 10), yIn, zIn + (yChunk * 10)), hit.collider.gameObject.GetComponent<MeshRenderer>().material
                    , new Vector2(3, 16), CalculateNeighbours(tileGeneratorScript.chunkArray[xChunk, yChunk][(xIn - 1), yIn, zIn], cubeSpawnScript.cubeArray));


                tileGeneratorScript.chunkArray[xChunk, yChunk][xIn, yIn, zIn] = null;
                Destroy(hit.transform.parent.gameObject);

            }
               
        }

    }

    //this function operates in a simmilar way to the neighbour function on Cubegenrator script but it doesnt use the heighmap values to check if there will be a neighbour
    //instead we look at adjacent places in the array to tell if there is already a cube fac ethere or not then if there is something there we wont spawn that face in on this new cube we are adding
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
