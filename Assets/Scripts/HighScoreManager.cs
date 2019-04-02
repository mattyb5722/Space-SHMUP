using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour {
    /// <summary>
    /// Manages the high scores
    /// </summary>

    public static HighScoreManager instance = null; // instance of this class

    private List<int> highScores = new List<int>(); // List of High Scores

    public void Awake()
    {
        // Sets up instance of this class
        if (instance != null) { Destroy(gameObject); }
        else { instance = this; }

        // Create PlayerPrefs for High Scores
        for (int i = 0; i < 5; i++) {
            string title = "High Score: " + i; // Name of the PlayerPref
            if (!PlayerPrefs.HasKey(title)) {
                PlayerPrefs.SetInt(title, 0); // Default Value
            }
            highScores.Add(PlayerPrefs.GetInt(title));
        }
    }
    // Sets new High Score
    public void NewHighScore(int score)
    {
        for (int i = 0; i < highScores.Count; i++) // Goes through high scores
        {
            if (score > highScores[i]) // Finds where the new score should be 
            {
                highScores.Insert(i, score); // Places new high score
                highScores.RemoveAt(highScores.Count - 1); // Remove lowest high score
                break;
            }
        }
        for (int i = 0; i < highScores.Count; i++) // Goes through high scores
        {
            PlayerPrefs.SetInt("High Score: " + i, highScores [i]); // Places new high score
        }
    }
    // Gets list of High Scores
    public List<int> getHighScores()
    {
        return highScores;
    }
}
