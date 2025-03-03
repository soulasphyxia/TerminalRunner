using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int currentLives = 3; // Текущее количество жизней
    public GameObject livePrefab; // Префаб сердечка
    public Transform livesContainer; // Контейнер для сердечек

    private Image[] lives; // Массив для хранения всех сердечек

    void Start()
    {
        if (livePrefab == null || livesContainer == null)
        {
            return;
        } 
        lives = new Image[currentLives];

        for (int i = 0; i < currentLives; i++)
        {
            // Создаем новое сердечко из префаба
            GameObject liveObject = Instantiate(livePrefab, livesContainer);
            lives[i] = liveObject.GetComponent<Image>();

            if (lives[i] == null)
            {
                continue;
            }

            float xPosition = i * (lives[i].rectTransform.rect.width + 5);
            lives[i].rectTransform.anchoredPosition = new Vector2(xPosition, 0);
        }
    }

    public void LoseLife()
    {
        if (currentLives > 0)
        {
            currentLives--;
            if (lives.Length > 0 && lives[currentLives] != null)
            {
                Destroy(lives[currentLives].gameObject);
            }
        }

        if (currentLives == 0)
        {
            Debug.Log("Game Over!");
        }
    }
}
