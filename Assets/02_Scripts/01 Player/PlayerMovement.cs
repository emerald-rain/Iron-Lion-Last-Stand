using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode; 

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody2D rb;  // Rigidbody component for 2D physics
    private float vertical;   // Vertical movement input
    private float horizontal; // Horizontal movement input
    private CameraMovement mainCamera; // Reference to the CameraMovement script

    [SerializeField] private float moveSpeed; // Movement speed for the player
    [SerializeField] private float speedLimit = 0.7f; // Speed limit for diagonal movement

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // Get the Rigidbody2D component attached to the same GameObject
        mainCamera = Camera.main.GetComponent<CameraMovement>();  // Get the CameraMovement component from the main camera
    }

    void Update()
    {
        // Only process movement for the owner of this object
        if (!IsOwner) return;

        // Handle the movement for the server authoritative player
        HandleMovementServerAuth();
    }

    void FixedUpdate()
    {
        // Set the player's transform for the camera (executed in FixedUpdate for physics-related updates)
        mainCamera.SetPlayer(transform);
    }

    private void HandleMovementServerAuth()
    {
        // Get the horizontal and vertical input from the player
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Log the input (for debugging purposes)
        // Debug.Log(horizontal + " " + vertical);

        // Call the server RPC to handle movement
        HandleMovementServerRpc(horizontal, vertical);
    }

    [ServerRpc(RequireOwnership = false)]
    private void HandleMovementServerRpc(float horizontal, float vertical)
    {
        // Apply speed limit if moving diagonally
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= speedLimit;
            vertical *= speedLimit;
        }

        // Set the velocity of the rigidbody for movement
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
}