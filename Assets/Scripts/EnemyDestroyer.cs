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
        if (IsOutOfBounds())
        {
            Destroy(gameObject);
            GameEvents.CallOnEnemyGetAway();
        }
    }

    private bool IsOutOfBounds()
    {
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        Vector3 enemyPosition = transform.position;

        return enemyPosition.y < -cameraHeight;
    }
}