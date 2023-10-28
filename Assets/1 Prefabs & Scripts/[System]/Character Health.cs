using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public Transform pfHealthBar; // Префаб полосы здоровья
    public float maxWidth = 0.8f; // Максимальная ширина полосы
    public float fixedHeight = 0.1f; // Фиксированная высота полосы
    public int maxHealth = 100; // Максимальное здоровье

    public HealthSystem healthSystem; // Система здоровья

    private void Start()
    {
        // Инициализируем систему здоровья с максимальным здоровьем
        healthSystem = new HealthSystem(maxHealth);
        CreateHealthBar(); // Создаем полосу здоровья

        GameObject healthBarObject = GameObject.Find("Bar");
        HealthBar healthBar = healthBarObject.GetComponent<HealthBar>();
        // Настраиваем полосу здоровья
        healthBar.Setup(healthSystem, maxWidth, fixedHeight);
    }

    private void CreateHealthBar()
    {
        Transform healthBarTransform = Instantiate(pfHealthBar, gameObject.transform.position + Vector3.up * 2, Quaternion.identity);
        // Устанавливаем игрока в качестве родителя полосы здоровья
        healthBarTransform.parent = gameObject.transform;
    }
}
