using UnityEngine;

public class PetorianFlag : MonoBehaviour
{
    public int scoreValue = 100; // Points awarded for collecting a flag

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(scoreValue); // Call GameManager to update score
            Destroy(gameObject); // Remove the flag after collection
        }
    }
}
