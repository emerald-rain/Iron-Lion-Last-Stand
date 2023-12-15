using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

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

    public void SpawnEnemys() {
        for (int i = 0; i < enemySpawnCount; i++) {
            // Выбор случайного префаба из списка
            GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];

            // Генерация случайной позиции в диапазоне spawnDistance от игрока
            Vector3 randomSpawnPosition = playerTransform.position + Random.insideUnitSphere * spawnDistance;
            randomSpawnPosition.z = 0; // Устанавливаем z-координату (предполагая плоскость)

            // Проверка минимального расстояния между игроком и врагами
            float distanceToPlayer = Vector3.Distance(playerTransform.position, randomSpawnPosition);
            if (distanceToPlayer < spawnDistance) {
                randomSpawnPosition = playerTransform.position + (randomSpawnPosition - playerTransform.position).normalized * spawnDistance;
            }

            // Получение ячейки тайлмапа в этой позиции
            Vector3Int spawnCell = groundTilemap.WorldToCell(randomSpawnPosition);

            // Проверка, что ячейка находится на тайлмапе и пуста
            if (groundTilemap.HasTile(spawnCell))
            {
                // Создание врага в выбранной позиции
                Instantiate(randomEnemyPrefab, randomSpawnPosition, Quaternion.identity);
            }
        }
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