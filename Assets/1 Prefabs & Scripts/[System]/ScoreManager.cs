using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro object

    void Start()
    {
        // Assuming you have assigned the TextMeshPro object in the Inspector
        if (scoreText != null)
        {
            UpdateScoreText();
        }
    }

    // Method to increase the score
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    // Method to decrease the score
    public void DecreaseScore(int amount)
    {
        score -= amount;
        // Ensure the score doesn't go below zero
        if (score < 0)
        {
            score = 0;
        }
        UpdateScoreText();
    }

    // Method to reset the score to zero
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    // Method to update the score text
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // Method to retrieve the current score
    public int GetScore()
    {
        return score;
    }
}
