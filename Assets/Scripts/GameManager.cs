using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] LifeManager lifeManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Camera mainCamera;

    private void Awake()
    {
        GameEvents.OnEnemyDestroyed += HandleEnemyDestroyed;
        GameEvents.OnEnemyGetAway += HandleEnemyGotAway;
    }

    public void HandleEnemyGotAway() {
        Debug.Log("enemy got away");
        lifeManager.LoseLife();
    }

    public void HandleEnemyDestroyed()
    {
        Debug.Log("enemy destroyed");
        DestroyNearestEnemyToCameraBottom();
        scoreManager.AddScore(100);
    }

    private void DestroyNearestEnemyToCameraBottom()
    {
        // Находим всех врагов на сцене
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.LogWarning("На сцене нет врагов!");
            return;
        }

        // Определяем позицию нижней границы камеры
        Vector3 cameraBottomPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane));

        // Переменные для хранения ближайшего врага и минимального расстояния
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        // Ищем ближайшего врага
        foreach (GameObject enemy in enemies)
        {
            // Вычисляем расстояние между врагом и нижней границей камеры
            float distance = Mathf.Abs(enemy.transform.position.y - cameraBottomPosition.y);

            // Если найден более близкий враг, обновляем данные
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // Уничтожаем ближайшего врага
        if (nearestEnemy != null)
        {
            Destroy(nearestEnemy.gameObject);
            Debug.Log("Уничтожен ближайший враг к нижней границе камеры.");
        }
        else
        {
            Debug.LogWarning("Не удалось найти ближайшего врага!");
        }
    }
}
