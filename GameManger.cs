using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int score = 0;
    public int bonusThreshold = 500;

    // System references
    private LivesSystem livesSystem;
    private ReplayLevels replaySystem;
    private LevelCompletion levelCompletion;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Initialize systems
        livesSystem = GetComponent<LivesSystem>();
        replaySystem = GetComponent<ReplayLevels>();
        levelCompletion = GetComponent<LevelCompletion>();
    }

    public void AddScore(int value)
    {
        score += value;
        Debug.Log("Score: " + score);
        CheckForBonus();
        levelCompletion.UpdateProgress(score);
    }

    private void CheckForBonus()
    {
        if (score >= bonusThreshold)
        {
            Debug.Log("Bonus unlocked!");
            livesSystem.AddExtraLife();
            replaySystem.UnlockReplay();
        }
    }

    public void RestartLevel()
    {
        if (livesSystem.HasLives())
        {
            livesSystem.DecrementLife();
            replaySystem.RestartCurrentLevel();
        }
        else
        {
            GameOver();
        }
    }

    public void CompleteLevel()
    {
        levelCompletion.MarkLevelComplete();
        replaySystem.UnlockNextLevel();
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Add game over logic
    }
}
