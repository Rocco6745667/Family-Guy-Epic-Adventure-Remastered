using UnityEngine;

public class DistractionEffect : MonoBehaviour
{
    public float duration = 3f; // Duration of the distraction effect

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                StartCoroutine(ApplyEffect(player));
            }
            Destroy(gameObject); // Remove the distraction after activation
        }
    }

    private IEnumerator ApplyEffect(PlayerController player)
    {
        // Apply a temporary effect, e.g., slowing the player
        player.moveSpeed /= 2f;
        Debug.Log("Player is disoriented!");
        yield return new WaitForSeconds(duration);
        player.moveSpeed *= 2f;
        Debug.Log("Player recovered from distraction!");
    }
}
