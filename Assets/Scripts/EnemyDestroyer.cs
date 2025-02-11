using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Проверяем, находится ли враг за пределами экрана
        if (IsOutOfBounds())
        {
            Destroy(gameObject); // Уничтожаем врага
        }
    }

    private bool IsOutOfBounds()
    {
        // Получаем границы экрана в мировых координатах
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 enemyPosition = transform.position;

        // Проверяем, вышел ли враг за нижний край экрана
        return enemyPosition.y < -cameraHeight;
    }
}