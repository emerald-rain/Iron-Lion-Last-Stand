using UnityEngine;
using UnityEngine.AI;

public class ChaserEnemy : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent agent;
    private Animator animator;
    
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    void Update() {
        if (target != null && agent.isActiveAndEnabled)
            agent.SetDestination(target.position);
            UpdateAnimationAndFlip();
        
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void UpdateAnimationAndFlip() {
        bool isWalking = agent.remainingDistance > agent.stoppingDistance;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            Vector3 direction = (agent.destination - transform.position).normalized;

            if (direction.x < 0) // LEFT
                transform.localScale = new Vector3(-1, 1, 1);
            else if (direction.x > 0) // RIGHT
                transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
