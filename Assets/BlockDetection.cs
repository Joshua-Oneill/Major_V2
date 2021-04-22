using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetection : MonoBehaviour
{
    public Camera playerCamera;
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
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); //creates a ray based on the position of the  mouse through the player Camera
        Debug.DrawRay(ray.origin, ray.direction * 30, Color.green);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 5))
            {
                Debug.Log(hit.collider.name);
                hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;

        }
    }

}
