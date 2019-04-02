using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHighScores : MonoBehaviour {
    /// <summary>
    /// Update UI values for high scores
    /// </summary>

	private void Update () {
        List<int> highScores = HighScoreManager.instance.getHighScores(); // List of high scores
        Text [] scores = GetComponentsInChildren<Text>(); // UI Text objects
        for (int i = 0; i < 5; i++)
        {
            scores[i].text = i+1 + ": " + highScores[i]; // Updates UI text objects
        }
    }
}
