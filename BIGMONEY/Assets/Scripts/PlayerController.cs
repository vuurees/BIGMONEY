using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody myRB;
    Camera playerCam;

    Vector2 camRotation;


    public float speed = 10.0f;
    public float jumpHeight = 5.0f;

    public float mouseSensitivity = 2.0f;
    public float Xsensitivity = 2f;
    public float Ysensitivity = 2f;
    public float camRotationLimit = 90f;

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

       
        if (Input.GetKeyDown(KeyCode.Space))
            temp.y = jumpHeight;

      
        myRB.velocity = (transform.forward * temp.z) + (transform.right * temp.x) + (transform.up * temp.y);
    }

}