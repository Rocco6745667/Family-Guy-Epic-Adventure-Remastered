using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int score = 0;
    public int bonusThreshold = 500; // Score required for bonus reward

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

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
            // Implement bonus reward logic here
        }
    }
}
