using UnityEngine;
using UnityEngine.AI;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float timeBetweenShots;
    
    private NavMeshAgent agent;
    private Animator animator;
    private float nextShotTime;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void Update()
    {
        if (target != null && agent.isActiveAndEnabled)
            agent.SetDestination(target.position);
            animator.SetBool("isWalking", agent.remainingDistance > agent.stoppingDistance);
            
        if (target != null && Time.time > nextShotTime)
        {
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
            nextShotTime = Time.time + timeBetweenShots;

            animator.SetBool("IsWalking", false);
        }
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
