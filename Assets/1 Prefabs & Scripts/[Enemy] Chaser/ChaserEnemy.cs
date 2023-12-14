using UnityEngine;
using UnityEngine.AI;

public class ChaserEnemy : MonoBehaviour
{
    [SerializeField] private float explosionPreparation = 0.5f;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private int explosionDamage = 50;
    [SerializeField] private SoundEffectsPlayer soundEffectsPlayer;

    private NavMeshAgent agent; // AI movement agent
    private Animator animator; // animation controller

    private Transform target;

    private bool isCountingDown;
    private float countdownTimer;
    
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            target = player.transform;
    }

    void Update() {
        if (target != null && agent.isActiveAndEnabled) {
            agent.SetDestination(target.position);
            UpdateAnimationAndFlip();

            bool isWithinStoppingDistance = agent.remainingDistance <= agent.stoppingDistance;
            if (isWithinStoppingDistance) {
                if (!isCountingDown) {
                    isCountingDown = true;
                    countdownTimer = 0f;
                }
                else {
                    countdownTimer += Time.deltaTime;
                    if (countdownTimer >= explosionPreparation) {
                        Transform phHealthBaer = transform.Find("pfHealthBar(Clone)");
                        if (phHealthBaer != null)
                            Destroy(phHealthBaer.gameObject); // Deleting hp bar
                        Explode();
                    }
                }
            }
            else { isCountingDown = false; }
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private void Explode() {
        soundEffectsPlayer.PlayRandom();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D collider in colliders){
            CharacterHealth characterHealth = collider.GetComponent<CharacterHealth>();
            if (characterHealth != null) {
                characterHealth.TakeDamage(explosionDamage);
            }
        }

        animator.SetTrigger("Explode");
        agent.enabled = false;
    }

    public void DestroyEnemy() {
        Destroy(gameObject);
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
