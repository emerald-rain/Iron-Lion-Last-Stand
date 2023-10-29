using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    private float maxWidth = 0.8f; // Максимальная ширина
    private float fixedHeight = 0.1f; // Фиксированная высота

    // Метод для настройки полосы здоровья
    public void Setup(HealthSystem healthSystem, float maxWidth, float fixedHeight)
    {
        this.healthSystem = healthSystem;
        this.maxWidth = maxWidth;
        this.fixedHeight = fixedHeight;

        // Подписываемся на событие изменения здоровья
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    // Обработчик события изменения здоровья
    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        float healthPercent = healthSystem.GetHealthPercent();

        // Вычисляем новый размер полосы здоровья с использованием параметров maxWidth и fixedHeight
        float newWidth = healthPercent * maxWidth;

        // Устанавливаем масштаб объекта полосы здоровья
        transform.localScale = new Vector3(newWidth, fixedHeight, 1);
    }
}
