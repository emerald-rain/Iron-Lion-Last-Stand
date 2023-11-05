using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float lifespan = 10.0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Convert the mouse position from screen coordinates to world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the direction vector from the bullet's position to the mouse position
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        rb.velocity = direction.normalized * force;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        Destroy(gameObject, lifespan);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            CharacterHealth characterHealth = other.GetComponent<CharacterHealth>();
            if (characterHealth != null) characterHealth.TakeDamage(20);
        }

        Destroy(gameObject);
    }
}
