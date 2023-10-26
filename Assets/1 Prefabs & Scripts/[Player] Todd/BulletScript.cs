using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos; // Stores the world position of the mouse click
    private Camera mainCam; // Reference to the main camera in the scene
    private Rigidbody2D rb; // Reference to the Rigidbody2D component of this object
    public float force; // Public variable for the bullet's initial force

    void Start()
    {
        // Find the main camera in the scene by tag
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Get the Rigidbody2D component attached to this GameObject
        rb = GetComponent<Rigidbody2D>();

        // Convert mouse position to world coordinates
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the bullet to the mouse
        Vector3 direction = mousePos - transform.position;

        // Calculate the rotation for the bullet
        Vector3 rotation = transform.position - mousePos;

        // Set the velocity of the bullet based on direction and force
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Calculate the angle of rotation and set it
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    }
}
