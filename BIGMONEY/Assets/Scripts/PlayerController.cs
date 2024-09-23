using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class AdvancedMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 5f;
    public float runMultiplyer;
    public float gravity = -9.81f;

    private Vector3 velocity;
    private Rigidbody rb;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;

    public LayerMask groundMask;

     void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        //check if player of ground
        isGrounded = Physics.CheckCapsule(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -2f; 
            
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        float speed = moveSpeed * (Input.GetKey(KeyCode.LeftShift) ? runMultiplyer : 1f);

        rb.MovePosition(rb.position + move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) ;

        velocity.y += gravity * Time.deltaTime;

        rb.MovePosition(rb.position + velocity * Time.deltaTime);

    }
}