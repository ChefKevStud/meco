using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] public float moveSpeed;

    public float groundDrag;

    public float jump;
    public float jumpRest;
    public float airMult;
    private bool jumpReady = true;

    [Header("Keybinds")] public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")] public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    private float horizontalIntput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);
        
        MyInput();
        SpeedControl();
        
        // drag
        if (grounded) rb.drag = groundDrag;
        else rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalIntput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && jumpReady && grounded)
        {
            jumpReady = false;
            Jump();
            
            Invoke(nameof(ResetJump), jumpRest);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalIntput;
        
        if(grounded) rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMult, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        // limit velocity
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        // resetting y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jump, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        jumpReady = true;
    }
}
