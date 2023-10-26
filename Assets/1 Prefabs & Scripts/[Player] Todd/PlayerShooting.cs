using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera mainCam;  // Reference to the main camera in the scene
    private Vector3 mousePos;  // Stores the world position of the mouse

    public GameObject bullet; // Prefab for the bullet
    public Transform bulletTransform; // Transform to specify bullet spawn position
    public bool canFire; // Flag indicating if the player can fire
    private float timer; // Timer for controlling firing rate
    public float timeBetweenFiring; // Time interval between shots

    void Start()
    {
        // Find the main camera in the scene by tag
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        // Convert mouse position to world coordinates
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the rotation of the gun towards the mouse
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            // Increment the timer
            timer += Time.deltaTime;

            // Check if enough time has passed to allow firing again
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        // Check for mouse button press and whether the player can fire
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            // Instantiate a bullet at the specified position
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
    }
}
