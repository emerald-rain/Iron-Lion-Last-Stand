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
        targetPosition = FindObjectOfType<PlayerMovement>().transform.position;
        direction = (targetPosition - transform.position).normalized;
        Destroy(gameObject, 5f);
        StartCoroutine(EnableCollision());
    }

    private void Update() 
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.1f);
        canCollide = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canCollide) return;  // Игнорируем столкновения, пока canCollide не станет true

        if (other.CompareTag("Player"))
        {
            print("should work");
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

