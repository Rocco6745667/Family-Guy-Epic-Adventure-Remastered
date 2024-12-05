using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; // Value of the coin
    public AudioClip collectSound; // Optional: sound effect for coin collection

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Update score
            GameManager.Instance.AddScore(coinValue);

            // Play collect sound
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }

            // Add visual effect (optional)
            PlayCollectEffect();

            // Destroy coin
            Destroy(gameObject);
        }
    }

    private void PlayCollectEffect()
    {
        // Placeholder for visual effect, e.g., particle system or animation
        Debug.Log("Coin collected!");
    }
}
