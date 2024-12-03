using UnityEngine;

public class JoeSwatsonBoss : MonoBehaviour
{
    public float health = 100f;
    public GameObject healthBarUI; // Reference to the health bar UI
    public Transform[] platforms; // Array of platforms Joe can move between
    public float moveSpeed = 3f;
    public float lungeSpeed = 10f;
    public GameObject rangedAttackPrefab; // Prefab for ranged attack
    public Transform rangedAttackSpawnPoint;

    private Transform player;
    private int currentPhase = 1;
    private bool isLunging = false;
    private bool isEnraged = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        UpdateHealthBar();
    }

    void Update()
    {
        if (health <= 0) return;

        switch (currentPhase)
        {
            case 1:
                PhaseOneBehavior();
                break;
            case 2:
                PhaseTwoBehavior();
                break;
            case 3:
                PhaseThreeBehavior();
                break;
        }
    }

    void PhaseOneBehavior()
    {
        // Basic melee movement
        MoveBetweenPlatforms();
        if (Random.Range(0, 100) < 5) LungeAtPlayer();
    }

    void PhaseTwoBehavior()
    {
        MoveBetweenPlatforms();
        if (Random.Range(0, 100) < 5) LungeAtPlayer();
        if (Random.Range(0, 100) < 10) RangedAttack();
    }

    void PhaseThreeBehavior()
    {
        if (!isEnraged)
        {
            isEnraged = true;
            moveSpeed *= 1.5f; // Increase speed
            Debug.Log("Joe Swatson is enraged!");
        }
        MoveBetweenPlatforms();
        if (Random.Range(0, 100) < 10) LungeAtPlayer();
        if (Random.Range(0, 100) < 20) RangedAttack();
    }

    void MoveBetweenPlatforms()
    {
        // Move Joe between platforms based on player position
        Transform targetPlatform = platforms[Random.Range(0, platforms.Length)];
        transform.position = Vector2.MoveTowards(transform.position, targetPlatform.position, moveSpeed * Time.deltaTime);
    }

    void LungeAtPlayer()
    {
        if (isLunging) return;
        isLunging = true;
        Vector2 direction = (player.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().velocity = direction * lungeSpeed;

        // Reset after a short delay
        Invoke(nameof(ResetLunge), 1f);
    }

    void ResetLunge()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isLunging = false;
    }

    void RangedAttack()
    {
        Instantiate(rangedAttackPrefab, rangedAttackSpawnPoint.position, Quaternion.identity);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
        else if (health <= 50 && currentPhase == 1)
        {
            currentPhase = 2; // Transition to phase 2
            Debug.Log("Joe Swatson is entering Phase 2!");
        }
        else if (health <= 25 && currentPhase == 2)
        {
            currentPhase = 3; // Transition to phase 3
            Debug.Log("Joe Swatson is entering Phase 3!");
        }
    }

    void UpdateHealthBar()
    {
        // Update the health bar UI (assumes a slider)
        if (healthBarUI)
        {
            healthBarUI.GetComponent<UnityEngine.UI.Slider>().value = health / 100f;
        }
    }

    void Die()
    {
        Debug.Log("Joe Swatson has been defeated!");
        Destroy(gameObject); // Destroy the boss
    }
}
