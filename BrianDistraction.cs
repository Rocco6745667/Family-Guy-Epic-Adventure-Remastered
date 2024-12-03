using UnityEngine;

public class BrainDistraction : MonoBehaviour
{
    public GameObject distractionPrefab; // Prefab for the distraction (e.g., an object or effect)
    public Transform[] spawnPoints; // Points where distractions can appear
    public float distractionInterval = 5f; // Time between distractions

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= distractionInterval)
        {
            CreateDistraction();
            timer = 0f;
        }
    }

    private void CreateDistraction()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(distractionPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        Debug.Log("Brain created a distraction!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Optional logic for direct interaction with the player
            Debug.Log("Brain is distracting the player!");
        }
    }
}
