using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float locamotion = 10f;
    public float sprint = 50f;
    Vector3 velocity;
    public float gravity = -9.81f;
    public Transform GC;
    public float groundD = 0.4f;
    public LayerMask GroundMask;
    bool isGrounded;
    public float JumpH = 3f;

    // Start is called before the first frame update
    void Start()
    {
        speed = locamotion;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(GC.position, groundD, GroundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetMouseButton(0))
        {
            // GetComponent<flight>().enabled = true;

        }
        else
        {
            // GetComponent<flight>().enabled = false;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Jump");

       

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = sprint;
        }    
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = locamotion;
        }

        Vector3 move = transform.right * x + transform.forward * z + (transform.up * y * JumpH);

        

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
