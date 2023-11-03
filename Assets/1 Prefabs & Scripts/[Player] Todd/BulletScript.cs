using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float lifespan = 5.0f;

    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private bool canCollide = false;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 90);

        StartCoroutine(DestroyBulletAfterDelay());
        StartCoroutine(EnableCollision());
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (!canCollide) return;

        if (other.CompareTag("Enemy")) {
            CharacterHealth characterHealth = other.GetComponent<CharacterHealth>();
            if (characterHealth != null) characterHealth.TakeDamage(20);
            Destroy(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }

    IEnumerator EnableCollision()
    {
        yield return new WaitForSeconds(0.1f);
        canCollide = true;
    }
}
