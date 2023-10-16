using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Transform player; // Reference to the player's transform

    public Vector3 offset; // Offset of the camera from the player
    public float speed; // Speed at which the camera follows the player

    // This method is called at a fixed rate, useful for physics-related updates
    void FixedUpdate()
    {
        if (player != null)
        {
            // Calculate the desired position of the camera
            Vector3 desiredPos = player.position + offset;

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPos, speed * Time.deltaTime);
        }
    }

    // Method to set the player for the camera to follow
    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer; // Set the player as the target for the camera
    }
}
