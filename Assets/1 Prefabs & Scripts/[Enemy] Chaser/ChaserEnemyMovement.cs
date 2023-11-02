using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChaserEnemyMovement : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;

    void Start()
{
    agent = GetComponent<NavMeshAgent>();
    agent.updateRotation = false;

    GameObject player = GameObject.FindWithTag("Player");
    if (player != null)
    {
        target = player.transform;
    }

    // Убедитесь, что агент размещен на NavMesh
    agent.Warp(transform.position);
}

void Update()
{
    if (target != null && agent.isActiveAndEnabled)
        agent.SetDestination(target.position);

    transform.rotation = Quaternion.Euler(0, 0, 0);
}

}