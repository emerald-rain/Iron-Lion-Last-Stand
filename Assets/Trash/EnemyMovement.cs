using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);

        // Displaying the enemy's aggression state in the editor
        if (distance < distanceBetween) {
            Debug.DrawLine(transform.position, player.transform.position, Color.red); 
        } else {
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }

        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance < distanceBetween) {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
}
