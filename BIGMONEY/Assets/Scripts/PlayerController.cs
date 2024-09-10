using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    

    Vector2 camRotation;


    //Movement
    Rigidbody myRB;
    public float speed = 10.0f;
    public float jumpHeight = 5.0f;
    public float grounddetectdistance = 1;

     //Sprint
     public float sprintMulitplyer = 2.5f;
     public Boolean sprintMode = false;
     public Boolean ToggleSprint = true;

     //Crouch
     public Boolean isCrouching = false;

     //Prone
     public Boolean isProne = false;

    //Camera
    Camera playerCam;
    public float mouseSensitivity = 2.0f;
    public float Xsensitivity = 2f;
    public float Ysensitivity = 2f;
    public float camRotationLimit = 90f;
    public float fieldofview = 90f;


    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        playerCam = transform.GetChild(0).GetComponent<Camera>();

        camRotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
     
        camRotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        camRotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

     
        camRotation.y = Mathf.Clamp(camRotation.y, -camRotationLimit, camRotationLimit);


        playerCam.transform.localRotation = Quaternion.AngleAxis(camRotation.y, Vector3.left);
        transform.localRotation = Quaternion.AngleAxis(camRotation.x, Vector3.up);

        
        Vector3 temp = myRB.velocity;

        temp.x = Input.GetAxisRaw("Horizontal") * speed;
        temp.z = Input.GetAxisRaw("Vertical") * speed;

        if (Input.GetKeyDown(KeyCode.LeftShift))
            sprintMode = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            sprintMode = false;


        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, grounddetectdistance))
            temp.y = jumpHeight;

      
        myRB.velocity = (transform.forward * temp.z) + (transform.right * temp.x) + (transform.up * temp.y);


    }

}