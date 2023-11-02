using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private Transform pfHealthBar; // Префаб полосы здоровья

    private float maxWidth = 0.8f; // Максимальная ширина полосы
    private float fixedHeight = 0.1f; // Фиксированная высота полосы
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0f, 1f, 0f); // Оффсет полосы здоровья
    [SerializeField] private int maxHealth = 100; // Максимальное здоровье

    public HealthSystem healthSystem; // Система здоровья

    private void Start()
    {
        // Инициализируем систему здоровья с максимальным здоровьем
        healthSystem = new HealthSystem(maxHealth);
        CreateHealthBar(); // Создаем полосу здоровья
    }

    private void CreateHealthBar()
    {
        // Определите смещение по горизонтали и вертикали относительно персонажа
        Vector3 offset = healthBarOffset;
        Transform healthBarTransform = Instantiate(pfHealthBar, gameObject.transform.position + offset, Quaternion.identity);
        // Устанавливаем игрока в качестве родителя полосы здоровья
        healthBarTransform.parent = gameObject.transform;

        // Получаем компонент HealthBar, связанный с этой конкретной полосой здоровья
        HealthBar healthBar = healthBarTransform.GetComponentInChildren<HealthBar>();
        // Настраиваем полосу здоровья
        healthBar.Setup(healthSystem, maxWidth, fixedHeight);
    }

    // Метод для обработки получения урона врагом
    public void TakeDamage(int damage) {
        healthSystem.Damage(damage);

        // Проверяем, если здоровье врага опустилось до нуля или меньше, то уничтожаем врага
        if (healthSystem.GetHealth() <= 0) Destroy(gameObject);
    }
}
