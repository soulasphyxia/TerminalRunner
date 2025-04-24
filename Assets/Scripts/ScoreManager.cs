using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0;
    public TMP_Text scoreText;

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Текстовый объект не назначен!");
        }
        else
        {
            UpdateScoreText();
        }
    }

    public void AddScore(int points)
    {
        currentScore += points; 
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }
}
