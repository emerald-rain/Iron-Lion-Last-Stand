using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; // Reference to the player's Rigidbody2D component
    private float vertical; // Store the vertical input value
    private float horizontal; // Store the horizontal input value

    public float moveSpeed; // Public variable to control the player's movement speed
    public float speedLimit = 0.7f; // Public variable to limit diagonal movement speed

    void Start()
    {
        // Get the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get horizontal and vertical input values
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Check if both horizontal and vertical movement is active and apply speed limit
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= speedLimit;
            vertical *= speedLimit;
        }

        // Set the velocity of the player based on input
        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
}
