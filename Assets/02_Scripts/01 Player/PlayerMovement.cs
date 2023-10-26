using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // https://www.youtube.com/watch?v=3Uc3cscnYns&t=53s&ab_channel=MoreBBlakeyyy

    private Rigidbody2D rb;
    float vertical;
    float horizontal;

    public float moveSpeed;
    public float speedLimit = 0.7f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        if(horizontal != 0 && vertical != 0) {
            horizontal *= speedLimit;
            vertical *= speedLimit;
        }

        rb.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }
}