using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Vector2 offset = new Vector2(1f, 0); // Смещение от центра персонажа
    private Vector2 direction; // Направление от персонажа к курсору
    private Transform playerTransform; // Трансформ персонажа

    void Start()
    {
        // Получаем трансформ персонажа (родителя)
        playerTransform = transform.parent;
    }

    void Update()
    {
        // Получаем позицию курсора в мировых координатах
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Вычисляем направление от персонажа к курсору
        direction = cursorPosition - (Vector2)playerTransform.position;

        // Обновляем позицию оружия с учетом смещения
        UpdateWeaponPosition();

        // Поворачиваем оружие в сторону курсора
        RotateWeapon();
    }

    void UpdateWeaponPosition()
    {
        // Вычисляем новую позицию оружия с учетом смещения
        Vector2 weaponPosition = (Vector2)playerTransform.position + offset;

        // Устанавливаем новую позицию оружия
        transform.position = weaponPosition;
    }

    void RotateWeapon()
    {
        // Вычисляем угол поворота оружия
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Определяем, в какую сторону поворачивается оружие
        bool isLeftSide = direction.x < 0;

        // Переворачиваем оружие по оси Y, если оно направлено влево
        transform.localScale = new Vector3(1f, isLeftSide ? -1f : 1f, 1f);

        // Поворачиваем оружие
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
