using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float sprintSpeed = 5f; // new sprint speed variable
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController controller;
    private Camera cam;
    private float verticalRotation = 0f;
    private bool isGrounded = false;
    private Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Handle mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up, mouseX);
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cam.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Handle player movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply sprinting
        if (Input.GetKey(KeyCode.LeftShift) && z > 0) // check if player is sprinting forward
        {
            moveSpeed = sprintSpeed; // use sprint speed if player is sprinting
        }
        else
        {
            moveSpeed = 3f; // use normal move speed
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }

        // Move player horizontally
        controller.Move(move * moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        isGrounded = controller.isGrounded;

        // Reset velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }
}
