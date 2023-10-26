using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform
    public Vector3 offset;  // Offset for camera position relative to the player
    public float speed; // Speed of camera movement

    void FixedUpdate()
    {
        // Calculate the desired camera position
        Vector3 desiredPos = player.position + offset;
        
        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.fixedDeltaTime);
    }
}
