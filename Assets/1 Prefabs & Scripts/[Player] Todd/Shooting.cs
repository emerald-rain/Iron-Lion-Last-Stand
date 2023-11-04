using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform weapon; // Transform объекта оружия
    public float timeBetweenFiring = 0.5f;
    public float offsetDistance = 1f; // Расстояние от персонажа до оружия
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        // Получаем позицию мыши в мировых координатах
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Вычисляем направление от персонажа к мыши
        Vector3 direction = mousePos - transform.position;

        // Позиционируем оружие на нужном расстоянии от персонажа
        Vector3 weaponPosition = transform.position + direction.normalized * offsetDistance;
        weaponPosition.z = 0;

        // Поворачиваем оружие к мыши
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle);

        // Применяем позицию и поворот оружия
        weapon.position = weaponPosition;
        weapon.rotation = rotation;

        // Проверяем нажатие кнопки мыши и таймер
        if (Input.GetMouseButton(0) && timer > timeBetweenFiring)
        {
            timer = 0;
            Instantiate(bullet, weapon.position, Quaternion.identity);
        }
    }
}
