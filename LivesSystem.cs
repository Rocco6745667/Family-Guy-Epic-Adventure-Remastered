using UnityEngine;
using UnityEngine.SceneManagement;

public class LivesSystem : MonoBehaviour
{
    public int lives = 3; // Initial number of lives
    public GameObject gameOverUI; // UI for game over screen

    public void LoseLife()
    {
        lives--;
        Debug.Log("Lives remaining: " + lives);

        if (lives <= 0)
        {
            GameOver();
        }
        else
        {
            RestartLevel();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        gameOverUI.SetActive(true); // Show Game Over UI
        Time.timeScale = 0f; // Pause the game
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current level
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f; // Resume time
        lives = 3; // Reset lives
        gameOverUI.SetActive(false); // Hide Game Over UI
        RestartLevel();
    }
}
