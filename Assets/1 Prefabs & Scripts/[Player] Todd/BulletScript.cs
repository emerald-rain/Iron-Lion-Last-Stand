using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float force = 50f;
    [SerializeField] private float lifespan = 5.0f;
    [SerializeField] private int strayFactor = 1;
    [SerializeField] private float minSpeedToRemove = 5f; // Новая переменная для минимальной скорости

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Convert the mouse position from screen coordinates to world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the direction vector from the bullet's position to the mouse position
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        float strayX = Random.Range(-strayFactor, strayFactor);
        float strayY = Random.Range(-strayFactor, strayFactor);
        direction += new Vector2(strayX, strayY).normalized * Random.Range(0f, 1f);

        rb.velocity = direction.normalized * force;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        Destroy(gameObject, lifespan);
    }

    void Update() { // destroy bullet if speed < 5
        if (rb.velocity.magnitude < minSpeedToRemove)
            Destroy(gameObject);
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
