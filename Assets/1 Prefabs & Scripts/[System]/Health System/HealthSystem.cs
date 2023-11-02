using System;
using UnityEngine;

public class HealthSystem
{
    // Событие для отслеживания изменения здоровья
    public event EventHandler OnHealthChanged;

    private int health; // Текущее здоровье
    private int healthMax; // Максимальное здоровье

    // Конструктор класса
    public HealthSystem(int healthMax)
    {
        this.healthMax = healthMax;
        // Устанавливаем начальное здоровье равным максимальному
        health = healthMax;
    }

    // Метод для получения текущего здоровья
    public int GetHealth() {
        return health;
    }

    // Метод для получения процента текущего здоровья относительно максимального
    public float GetHealthPercent() {
        return (float) health / healthMax;
    }

    // Метод для нанесения урона здоровью
    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health < 0) health = 0; // Защита от отрицательного здоровья
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty); // Генерируем событие изменения здоровья
    }

    // Метод для восстановления здоровья
    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > healthMax) health = healthMax; // Защита от превышения максимального здоровья
        if (OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty); // Генерируем событие изменения здоровья
    }
}
