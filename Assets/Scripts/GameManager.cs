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
        // ������� ���� ������ �� �����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.LogWarning("�� ����� ��� ������!");
            return;
        }

        // ���������� ������� ������ ������� ������
        Vector3 cameraBottomPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane));

        // ���������� ��� �������� ���������� ����� � ������������ ����������
        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        // ���� ���������� �����
        foreach (GameObject enemy in enemies)
        {
            // ��������� ���������� ����� ������ � ������ �������� ������
            float distance = Mathf.Abs(enemy.transform.position.y - cameraBottomPosition.y);

            // ���� ������ ����� ������� ����, ��������� ������
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        // ���������� ���������� �����
        if (nearestEnemy != null)
        {
            Destroy(nearestEnemy.gameObject);
            Debug.Log("��������� ��������� ���� � ������ ������� ������.");
        }
        else
        {
            Debug.LogWarning("�� ������� ����� ���������� �����!");
        }
    }
}
