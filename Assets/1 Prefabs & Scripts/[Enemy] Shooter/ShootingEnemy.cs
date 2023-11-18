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

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    private void Update()
    {
        if (target != null && agent.isActiveAndEnabled)
            agent.SetDestination(target.position);
            UpdateAnimationAndFlip();
            
        if (target != null && Time.time > nextShotTime && agent.velocity.magnitude < 0.1f)
        {
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
            Instantiate(projectile, transform.position, Quaternion.Euler(0, 0, angle));
            nextShotTime = Time.time + timeBetweenShots;
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void UpdateAnimationAndFlip()
    {
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
