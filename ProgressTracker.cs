using UnityEngine;
using UnityEngine.UI;

public class ProgressTracker : MonoBehaviour
{
    public GameObject levelProgressPrefab; // Prefab for each level's progress display
    public Transform progressContainer; // Parent object to hold progress UI elements

    public int totalLevels = 5; // Total number of levels
    public int[] totalDocumentsPerLevel; // Array to store the total documents in each level

    private void Start()
    {
        DisplayProgress();
    }

    private void DisplayProgress()
    {
        // Clear existing progress UI
        foreach (Transform child in progressContainer)
        {
            Destroy(child.gameObject);
        }

        // Populate progress UI
        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject progressItem = Instantiate(levelProgressPrefab, progressContainer);
            Text progressText = progressItem.GetComponentInChildren<Text>();

            int collected = PlayerPrefs.GetInt($"Level_{i}_Documents", 0);
            int total = i <= totalDocumentsPerLevel.Length ? totalDocumentsPerLevel[i - 1] : 0;

            progressText.text = $"Level {i}: Collected Documents {collected}/{total}";
        }
    }
}
