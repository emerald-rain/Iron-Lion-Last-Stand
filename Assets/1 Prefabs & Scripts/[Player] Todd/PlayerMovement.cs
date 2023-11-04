using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontal;
    private float vertical;
    public float moveSpeed = 5f;
    public float speedLimit = 0.7f;
    private string currentState;
    const string PLAYER_IDLE = "player_idle";
    const string PLAYER_WALK = "player_walk";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // Set the animation state and flip the sprite in the direction of movement
        SetMovementAnimation(horizontal, vertical);
    }

    void FixedUpdate()
    {
        Vector2 limitedSpeed = new Vector2(horizontal, vertical) * (horizontal != 0 && vertical != 0 ? speedLimit : 1f);
        rb.velocity = limitedSpeed * moveSpeed;
    }

    void SetMovementAnimation(float horizontal, float vertical)
    {
        // Determine whether the player is walking or idle
        bool isWalking = horizontal != 0 || vertical != 0;
        ChangeAnimationState(isWalking ? PLAYER_WALK : PLAYER_IDLE);
        
        // If the player is walking, flip the sprite based on the direction
        if (isWalking)
        {
            FlipSprite(horizontal);
        }
    }

    void FlipSprite(float horizontal)
    {
        if (horizontal != 0)
        {
            // Get the SpriteRenderer component and flip it on the x-axis.
            GetComponent<SpriteRenderer>().flipX = horizontal < 0;
        }
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}
