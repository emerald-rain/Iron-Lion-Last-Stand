using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float force; // Публичная переменная для начальной силы пули
    [SerializeField] private float lifespan = 5.0f; // Время жизни пули в секундах

    private Vector3 mousePos; // Хранит мировое положение щелчка мыши
    private Camera mainCam; // Ссылка на основную камеру в сцене
    private Rigidbody2D rb; // Ссылка на компонент Rigidbody2D этого объекта

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

        // Запустить корутину для уничтожения пули после заданного времени
        StartCoroutine(DestroyBulletAfterDelay());
    }

    // Столкновение с объектом.
    void OnTriggerEnter2D(Collider2D other) {
        // столкновение с объектом с тагом Enemy
        if (other.CompareTag("Enemy")) {
            // Получаем компонент EnemyHealth у врага (предполагается, что такой компонент существует)
            CharacterHealth characterHealth = other.GetComponent<CharacterHealth>();

            // Засчитываем урон, если у объекта не 0 хп.
            if (characterHealth != null) characterHealth.TakeDamage(20);

            // Уничтожаем пулю
            Destroy(gameObject);
        }
        // Если пуля столкнулась с чем-то другим, тоже уничтожаем её
        else Destroy(gameObject);
    }

    // Корутина для уничтожения пули после заданного времени
    private IEnumerator DestroyBulletAfterDelay()
    {
        yield return new WaitForSeconds(lifespan);
        Destroy(gameObject);
    }
}
