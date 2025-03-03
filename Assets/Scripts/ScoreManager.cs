using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int currentScore = 0; // Текущее количество очков
    public TMP_Text scoreText; // Ссылка на текстовый объект

    void Start()
    {
        if (scoreText == null)
        {
            Debug.LogError("Текстовый объект не назначен!");
        }
        else
        {
            UpdateScoreText(); // Обновляем текст при старте
        }
    }

    // Метод для увеличения очков
    public void AddScore(int points)
    {
        currentScore += points; // Увеличиваем очки
        UpdateScoreText(); // Обновляем текст
    }

    // Метод для обновления текста
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }
    }
}
