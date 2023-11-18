using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float horizontal, vertical; 
    public float movementSpeed = 5f; // normal straight line speed
    public float speedLimit = 0.7f; // slows down diagonal movement in percent

    private Animator animator; // on player animator
    private string currentState; // current animation state
    const string PLAYER_IDLE = "player_idle"; // idle animation
    const string PLAYER_WALK = "player_walk"; // walk animation

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        
        // Limiting the speed when moving diagonally
        if (movement.magnitude >= speedLimit)
        {
            movement = movement.normalized * speedLimit;
        }

        MovePlayer(movement);

        // Update animation
        UpdateAnimation(movement);
    }

    void MovePlayer(Vector2 movement)
    {
        rb.velocity = new Vector2(movement.x * movementSpeed, movement.y * movementSpeed);
    }

    void UpdateAnimation(Vector2 movement)
    {
        if (movement != Vector2.zero)
        {
            ChangeAnimationState(PLAYER_WALK);
        }
        else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }
    }

    void ChangeAnimationState(string newState)
    {
        // Stop the same animation from interrupting itself
        if (currentState == newState) return;

        // Play the new animation
        animator.Play(newState);

        // Reassign the current state
        currentState = newState;
    }
}
