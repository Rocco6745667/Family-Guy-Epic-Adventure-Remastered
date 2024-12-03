using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletion : MonoBehaviour
{
    public int requiredCollectibles = 5; // Number of collectibles needed to finish the level
    private int currentCollectibles = 0;

    public GameObject levelCompleteUI; // UI to display on level completion

    private bool levelCompleted = false;

    public void CollectItem()
    {
        currentCollectibles++;
        if (currentCollectibles >= requiredCollectibles)
        {
            Debug.Log("All required collectibles gathered!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && currentCollectibles >= requiredCollectibles && !levelCompleted)
        {
            CompleteLevel();
        }
    }

    void CompleteLevel()
    {
        levelCompleted = true;
        levelCompleteUI.SetActive(true); // Show level complete screen
        Time.timeScale = 0f; // Pause the game
    }

    public void NextLevel()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Load the next level
    }

    public void ReplayLevel()
    {
        Time.timeScale = 1f; // Resume time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current level
    }
}
