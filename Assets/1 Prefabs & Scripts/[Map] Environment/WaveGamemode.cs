using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class WaveGamemode : MonoBehaviour
{
    [Header("Tilemap to spawn on and enemy prefabs")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private List<GameObject> enemyPrefabs;

    [Header("Distance from player")]
    [SerializeField] private float minDistance = 17f;
    [SerializeField] private float maxDistance = 30f;

    [Header("Time beetween spawning and enemys")]
    [SerializeField] private float minSpawnTime = 20f; 
    [SerializeField] private float maxSpawnTime = 30f;
    [SerializeField] private int enemiesToSpawn = 3;

    private Transform playerTransform;
    private float spawnTimer;

    private void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        groundTilemap = GameObject.Find("[Walkable] [0] Basic ground").GetComponent<Tilemap>();
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
            Vector3 spawnPosition;
            bool positionFound = false;

            while (!positionFound) {
                float randomDistance = Random.Range(minDistance, maxDistance);
                Vector3 randomDirection = Random.onUnitSphere;
                randomDirection.z = 0;

                spawnPosition = playerTransform.position + randomDirection * randomDistance;
                Vector3Int tilePosition = groundTilemap.WorldToCell(spawnPosition); 

                if (groundTilemap.HasTile(tilePosition)) { 
                    positionFound = true;
                    int randomIndex = Random.Range(0, enemyPrefabs.Count);
                    GameObject prefabToSpawn = enemyPrefabs[randomIndex];
                    Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
                }
            }
        }
    }
}
