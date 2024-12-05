using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Score and Bonus
    private int score = 0;
    public int bonusThreshold = 500;

    // Diplomatic Documents
    public int totalDocuments = 5;
    private int collectedDocuments = 0;
    public Text documentsUI; // UI to display document progress
    public GameObject endpoint; // Locked endpoint initially

    // Lives
    public int lives = 3;
    public GameObject gameOverUI; // UI for Game Over screen

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateDocumentsUI();
        LockEndpoint();
    }

    // Document Collection
    public void CollectDocument()
    {
        collectedDocuments++;
        UpdateDocumentsUI();

        if (collectedDocuments >= totalDocuments)
        {
            UnlockEndpoint();
        }
    }

    private void UpdateDocumentsUI()
    {
        if (documentsUI != null)
        {
            documentsUI.text = $"Documents: {collectedDocuments}/{totalDocuments}";
        }
    }

    private void LockEndpoint()
    {
        if (endpoint != null)
        {
            endpoint.SetActive(false); // Deactivate endpoint initially
        }
    }

    private void UnlockEndpoint()
    {
        if (endpoint != null)
        {
            endpoint.SetActive(true); // Activate endpoint
            Debug.Log("All documents collected! Endpoint unlocked.");
        }
    }

    // Lives System
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

    public void AddExtraLife()
    {
        lives++;
        Debug.Log("Extra life granted! Total lives: " + lives);
    }

    // Score System
    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
        CheckForBonus();
    }

    private void CheckForBonus()
    {
        if (score >= bonusThreshold)
        {
            Debug.Log("Bonus unlocked!");
            AddExtraLife();
            UnlockReplay();
        }
    }

    // Level Completion
    public void CompleteLevel()
    {
        PlayerPrefs.SetInt($"Level_{currentLevel}_Documents", collectedDocuments);
        PlayerPrefs.Save();
        Debug.Log($"Level {currentLevel} progress saved!");
    }

    // Replay System
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current level
    }

    public void UnlockReplay()
    {
        Debug.Log("Replay unlocked!");
        // Logic to unlock replay functionality for the current level
    }

    public void UnlockNextLevel()
    {
        Debug.Log("Next level unlocked!");
        // Logic to unlock the next level
    }

    // Game Over
    private void GameOver()
    {
        Debug.Log("Game Over!");
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        Time.timeScale = 0f; // Pause the game
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        lives = 3; // Reset lives
        gameOverUI.SetActive(false);
        RestartLevel();
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Progress reset!");
    }

    public void AddScore(int value)
    {
    score += value;
    Debug.Log("Score: " + score);

    // Check for bonuses or other logic
    CheckForBonus();
    }

}
