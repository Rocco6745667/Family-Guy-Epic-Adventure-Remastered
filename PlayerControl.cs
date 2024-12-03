using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 7f;
    public float slideSpeed = 10f;
    public float slideDuration = 1f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = true;
    private bool isRunning = false;
    private bool isSliding = false;
    private float slideTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isSliding)
        {
            Slide();
            return; // Prevent other actions while sliding
        }

        Move();
        Jump();
        HandleSlideInput();
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift);

        float speed = isRunning ? runSpeed : walkSpeed;
        Vector2 velocity = new Vector2(moveInput * speed, rb.velocity.y);
        rb.velocity = velocity;

        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1); // Flip sprite
        }

        // Update animation state
        animator.SetBool("isRunning", isRunning);
        animator.SetFloat("speed", Mathf.Abs(moveInput));
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;

            // Update animation state
            animator.SetTrigger("jump");
        }
    }

    private void HandleSlideInput()
    {
        if (Input.GetKeyDown(KeyCode.C) && isGrounded && !isSliding) // 'C' for slide
        {
            isSliding = true;
            slideTimer = slideDuration;

            // Update animation state
            animator.SetTrigger("slide");
        }
    }

    private void Slide()
    {
        slideTimer -= Time.deltaTime;
        rb.velocity = new Vector2(transform.localScale.x * slideSpeed, rb.velocity.y);

        if (slideTimer <= 0)
        {
            isSliding = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void RechargeStamina()
{
    // Implement stamina recharge logic here
    Debug.Log("Stamina recharged!");
}

public void ActivateSpeedBoost(float duration)
{
    StartCoroutine(SpeedBoostCoroutine(duration));
}

private IEnumerator SpeedBoostCoroutine(float duration)
{
    float originalSpeed = runSpeed;
    runSpeed *= 1.5f; // Increase run speed by 50%
    Debug.Log("Speed boost activated!");
    yield return new WaitForSeconds(duration);
    runSpeed = originalSpeed;
    Debug.Log("Speed boost ended.");
}

public void ActivateInvincibility(float duration)
{
    StartCoroutine(InvincibilityCoroutine(duration));
}

private IEnumerator InvincibilityCoroutine(float duration)
{
    Debug.Log("Invincibility activated!");
    // Logic to ignore damage or effects (e.g., disable collision with enemies)
    yield return new WaitForSeconds(duration);
    Debug.Log("Invincibility ended.");
    // Revert changes
}

}
