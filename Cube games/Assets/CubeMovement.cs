using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5.0f;   // Movement speed
    [SerializeField] public float sprintSpeed = 10.0f; // Sprint speed
    [SerializeField] public float jumpForce = 5.0f;    // Jump force
    
    [SerializeField] public float groundCheckDistance = 1f; // Distance to check for ground

    private bool isGrounded;          // Ground check flag
    public Rigidbody rb;
    public LayerMask ground; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the cube is grounded
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance,ground);

        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        // Apply sprint speed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDirection *= sprintSpeed;
        }
        else
        {
            moveDirection *= moveSpeed;
        }

        // Apply movement
        Vector3 movement = moveDirection * Time.deltaTime;
        transform.Translate(movement);

        // Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
