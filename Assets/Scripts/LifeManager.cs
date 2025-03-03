using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int currentLives = 3; // ������� ���������� ������
    public GameObject livePrefab; // ������ ��������
    public Transform livesContainer; // ��������� ��� ��������

    private Image[] lives; // ������ ��� �������� ���� ��������

    void Start()
    {
        if (livePrefab == null || livesContainer == null)
        {
            return;
        } 
        lives = new Image[currentLives];

        for (int i = 0; i < currentLives; i++)
        {
            // ������� ����� �������� �� �������
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
