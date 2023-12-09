using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    Vector3 targetPosition;
    Vector3 direction;
    private bool canCollide = false;

    private void Start()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        if (player != null)
        {
            targetPosition = player.transform.position;
            direction = (targetPosition - transform.position).normalized;

            // Отримання нового напрямку, щоб куля рухалась у бік цілі
            Vector3 lookDirection = targetPosition - transform.position;
            transform.up = lookDirection;

            Destroy(gameObject, 5f);
            StartCoroutine(EnableCollision());
        }
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.1f);
        canCollide = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canCollide) return;

        if (other.CompareTag("Player"))
        {
            print("Shooter hits the player and deals 20 damage.");
            CharacterHealth characterHealth = other.GetComponent<CharacterHealth>();
            if (characterHealth != null) characterHealth.TakeDamage(20);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
