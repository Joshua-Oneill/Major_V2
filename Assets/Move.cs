using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    public float MouseSense = 100f;
    public Transform Player;
    private float Xrot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSense * Time.deltaTime;
        Xrot -= mouseY;
        Xrot = Mathf.Clamp(Xrot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(Xrot, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }
}
