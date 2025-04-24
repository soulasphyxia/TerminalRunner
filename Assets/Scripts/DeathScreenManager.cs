using TMPro;
using UnityEngine;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = $"Итоговый счет: {ScoreTransitioner.LastScore}";
    }
}
