using UnityEngine;

public class StewieBlocker : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of Stewie's movement
    public Transform pointA; // Start position
    public Transform pointB; // End position

    private bool movingToB = true;

    void Update()
    {
        MoveBetweenPoints();
    }

    private void MoveBetweenPoints()
    {
        Transform target = movingToB ? pointB : pointA;
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            movingToB = !movingToB; // Switch direction
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Logic to block player progress (e.g., play an animation or push back the player)
            Debug.Log("Stewie is blocking the way!");
        }
    }
}
