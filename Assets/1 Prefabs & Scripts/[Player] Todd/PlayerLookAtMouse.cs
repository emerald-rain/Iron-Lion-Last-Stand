using UnityEngine;

public class PlayerLookAtMouse : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Получаем компонент SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Получить позицию мыши в мировых координатах
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Поворачиваем персонажа только по оси X
        if (mousePos.x > transform.position.x)
        {
            // Повернуть персонажа вправо
            spriteRenderer.flipX = false;
        }
        else if (mousePos.x < transform.position.x)
        {
            // Повернуть персонажа влево
            spriteRenderer.flipX = true;
        }
    }
}
