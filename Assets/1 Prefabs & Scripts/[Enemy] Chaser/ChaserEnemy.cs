using UnityEngine;
using UnityEngine.AI;

public class ChaserEnemy : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
    
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void Update()
    {
        if (target != null && agent.isActiveAndEnabled)
        {
            agent.SetDestination(target.position);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
