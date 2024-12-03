using UnityEngine;

public class ProgressTracker : MonoBehaviour
{
    public string levelName; // Name of the level

    public void SaveProgress(int score)
    {
        PlayerPrefs.SetInt(levelName + "_Score", score); // Save the level's score
        PlayerPrefs.SetInt(levelName + "_Completed", 1); // Mark the level as completed
        PlayerPrefs.Save();
        Debug.Log("Progress saved for " + levelName);
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt(levelName + "_Score", 0);
    }

    public bool IsLevelCompleted()
    {
        return PlayerPrefs.GetInt(levelName + "_Completed", 0) == 1;
    }
}
