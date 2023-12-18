using UnityEngine;

public class SpawnPrefabOnScroll : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float minDistance = 20f;
    [SerializeField] private float maxDistance = 20f;
    private Transform playerTransform;

    private void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            float randomDistance = Random.Range(minDistance, maxDistance);
            Vector3 randomPosition = Random.onUnitSphere * randomDistance;

            // Сбрасываем Z-координату
            randomPosition.z = 0.7113333f;

            Vector3 spawnPosition = playerTransform.position + randomPosition;
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(playerTransform.position, minDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, maxDistance);
        }
    }
}
