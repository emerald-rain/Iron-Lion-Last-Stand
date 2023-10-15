using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode; 

public class PlayerMovement : NetworkBehaviour
{
    private Rigidbody2D rb;
    private float vertical;
    private float horizontal;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedLimit = 0.7f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Only process movement for the owner of this object
        if (!IsOwner) return;

        // Handle the movement for the server authoritative player
        HandleMovementServerAuth();
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
