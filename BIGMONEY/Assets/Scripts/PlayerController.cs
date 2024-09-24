using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class AdvancedMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float sprintMultiplyer = 1.5f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;
    public float groundCheckOffset = 0.1f;



    private Vector3 velocity;
    private Rigidbody rb;
    private bool isGrounded;

 
   // capsule collider parameters
    public LayerMask groundMask;
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>(); // get the capsule collider componet 


    }

    void Update()
    {
        //check if player is grounded
        CheckIfGrounded();

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; //reset velocity when grounded
        }

        //get movement inputs

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplyer : 1f);

        rb.MovePosition(rb.position + move * speed * Time.deltaTime);

        //jump

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); // jump
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }
    void CheckIfGrounded()
    {
         

        //define the bottom and top points of the capsule for ground detection
        Vector3 capsuleBottom = transform.position - Vector3.up * (capsuleCollider.height / 2f - capsuleCollider.radius)+Vector3.up * groundCheckOffset;
        Vector3 capsuleTop = transform.position - Vector3.up * (capsuleCollider.height /2f - capsuleCollider.radius);
        // check if the capsule is intersecting with any colliders on the groundmask layer
        isGrounded = Physics.CheckCapsule(capsuleBottom, capsuleTop, capsuleCollider.radius * 0.9f, groundMask);

        }

    }
