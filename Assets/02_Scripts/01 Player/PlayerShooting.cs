using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerShooting : NetworkBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    public GameObject bullet;  // Prefab for the bullet
    public Transform bulletTransform;  // Transform to spawn the bullet

    public bool canFire;  // Flag indicating if the player can fire
    private float timer;  // Timer to control firing rate
    public float timeBetweenFiring;  // Time between each firing

    void Start()
    {
        // Get the main camera
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void Update()
    {
        if (!IsOwner) return;

        // Get the mouse position in the game world
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        // Calculate rotation angle for aiming
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            // Increment the timer until the player can fire again
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                // Player can fire again
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;  // Player fired
            ShotServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void ShotServerRpc()
    {
            GameObject spawnedBulletObject = Instantiate(bullet, transform.position, Quaternion.identity);
            Transform spawnedBulletTransform = spawnedBulletObject.transform;

            // Spawn the bullet over the network
            spawnedBulletTransform.GetComponent<NetworkObject>().Spawn(true);
    }
}
