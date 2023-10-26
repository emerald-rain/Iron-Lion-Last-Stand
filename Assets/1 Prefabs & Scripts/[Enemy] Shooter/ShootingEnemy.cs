using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private Transform target;  // Reference to the player's Transform (target)
    [SerializeField] private GameObject projectile;  // Projectile to be instantiated
    [SerializeField] private float timeBetweenShots;  // Time interval between shots
    
    private float nextShotTime;  // Next time when the enemy can shoot

    private void Update()
    {
        if (Time.time > nextShotTime) // Check if it's time to shoot
        {
            // Instantiate a projectile at the enemy's position
            Instantiate(projectile, transform.position, Quaternion.identity);

            // Update the next shot time
            nextShotTime = Time.time + timeBetweenShots;
        }
    }
}
