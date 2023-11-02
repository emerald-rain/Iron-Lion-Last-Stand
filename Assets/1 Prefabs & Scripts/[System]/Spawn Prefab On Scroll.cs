using UnityEngine;

public class SpawnPrefabOnScroll : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float minDistance = 5.0f;
    public float maxDistance = 10.0f;

    private Transform playerTransform;

    private void Start()
    {
        // Get the player's transform (you can adjust this to your game's setup)
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        // Check if the mouse wheel is scrolled up
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            // Calculate a random position within the specified range
            float randomDistance = Random.Range(minDistance, maxDistance);
            Vector3 randomPosition = Random.onUnitSphere * randomDistance;

            // Offset the position to be relative to the player
            Vector3 spawnPosition = playerTransform.position + randomPosition;

            // Spawn the prefab at the calculated position
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.position, minDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(playerTransform.position, maxDistance);
    }
}
