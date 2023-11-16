using UnityEngine;
using System.Collections.Generic;

public class WaveGamemode : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs; // Список префабов врагов
    [SerializeField] private float minDistance = 17f;
    [SerializeField] private float maxDistance = 30f;
    [SerializeField] private float minSpawnTime = 20f; // Минимальное время для спавна врагов
    [SerializeField] private float maxSpawnTime = 30f; // Максимальное время для спавна врагов
    [SerializeField] private int enemiesToSpawn = 3; // Количество врагов для спавна

    private Transform playerTransform;
    private float spawnTimer;

    private void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void Update() {
        if (enemyPrefabs.Count == 0) return;

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0) {
            SpawnEnemies();
            spawnTimer = Random.Range(minSpawnTime, maxSpawnTime);
        }
    }

    private void SpawnEnemies() {
        for (int i = 0; i < enemiesToSpawn; i++) {
            float randomDistance = Random.Range(minDistance, maxDistance);
            Vector3 randomDirection = Random.onUnitSphere;
            randomDirection.z = 0; // Сбрасываем Z-координату

            Vector3 spawnPosition = playerTransform.position + randomDirection * randomDistance;

            int randomIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject prefabToSpawn = enemyPrefabs[randomIndex];

            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}
