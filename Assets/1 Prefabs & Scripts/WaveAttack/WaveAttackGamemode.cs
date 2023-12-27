using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.AI;

public class WaveAttackGamemode : MonoBehaviour
{
    [Header("Tilemap to spawn on and enemys prefabs")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private List<GameObject> enemyPrefabs;

    [Header("Enemys spawn logic settings")]
    [SerializeField] private int enemySpawnCount;
    [SerializeField] private float spawnTimeout;
    [SerializeField] private float timeoutStep;
    [SerializeField] private float minTimeout;
    [SerializeField] private int previousExtraHealth;
    [SerializeField] private int increaceExtraHealth;


    private Transform playerTransform;
    private float spawnDistance = 20f;
    private float timeUntilNextSpawn;

    public void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        groundTilemap = GameObject.Find("[Walkable] [0] Basic ground").GetComponent<Tilemap>();
        // Устанавливаем начальное значение для отслеживания времени до следующего спавна
        timeUntilNextSpawn = spawnTimeout;
    }

    public void Update() {
        // Уменьшаем время до следующего спавна
        timeUntilNextSpawn -= Time.deltaTime;

        if (timeUntilNextSpawn <= 0) {
            SpawnEnemys();
            SetNewTimeout();
            // Сбрасываем время до следующего спавна обратно на начальное значение
            timeUntilNextSpawn = spawnTimeout;
        }
    }

    public void SpawnEnemys()
    {
        for (int i = 0; i < enemySpawnCount; i++)
        {
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            Vector3 randomSpawnPosition = GetRandomNavMeshPosition();

            // Создание врага в выбранной позиции
            GameObject enemyInstance = Instantiate(randomEnemyPrefab, randomSpawnPosition, Quaternion.identity);

            // Получение компонента здоровья для изменения максимального здоровья
            ChaserHealth chaserHealth = enemyInstance.GetComponent<ChaserHealth>();
            ShooterHealth shooterHealth = enemyInstance.GetComponent<ShooterHealth>();

            if (chaserHealth != null)
            {
                // Увеличиваем максимальное здоровье каждого Chaser через 30 секунд
                chaserHealth.maxHealth += previousExtraHealth; // Измените этот параметр по вашему усмотрению
            }

            if (shooterHealth != null)
            {
                // Увеличиваем максимальное здоровье каждого Shooter через 30 секунд
                shooterHealth.maxHealth += previousExtraHealth; // Измените этот параметр по вашему усмотрению
            }
        }
        previousExtraHealth += increaceExtraHealth;
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        int attempts = 0;
        Vector3 randomSpawnPosition = Vector3.zero;

        do
        {
            // Генерация случайной позиции в диапазоне spawnDistance от игрока
            randomSpawnPosition = playerTransform.position + Random.insideUnitSphere * spawnDistance;
            randomSpawnPosition.z = 0;

            // Проверка минимального расстояния между игроком и врагами
            float distanceToPlayer = Vector3.Distance(playerTransform.position, randomSpawnPosition);
            if (distanceToPlayer < spawnDistance)
            {
                randomSpawnPosition = playerTransform.position + (randomSpawnPosition - playerTransform.position).normalized * spawnDistance;
            }

            // Проверка, что точка находится на NavMesh
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomSpawnPosition, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                break; // Выход из цикла, если точка находится на NavMesh
            }

            attempts++;
        } while (attempts < 30); // Ограничение на количество попыток для избежания бесконечного цикла

        return randomSpawnPosition;
    }

    public void SetNewTimeout() {
        if (spawnTimeout > minTimeout) {
            spawnTimeout -= timeoutStep; // Уменьшаем время появления следующией волны
        } else { // Если время достигло минимального значения, устанавливаем его на minTimeout
            spawnTimeout = minTimeout;
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(playerTransform.position, Vector3.forward, spawnDistance);
    }
    #endif
}