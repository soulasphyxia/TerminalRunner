using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    [SerializeField] LifeManager lifeManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject defaultExplosionPrefab;

    private void Awake()
    {
        GameEvents.OnEnemyDestroyed += (enemy) => HandleEnemyDestroyed(enemy);
        GameEvents.OnEnemyGetAway += HandleEnemyGotAway;
        GameEvents.GameOver += HandleGameOver;
    }

    public void HandleEnemyGotAway() {
        lifeManager.LoseLife();
    }

    public void HandleEnemyDestroyed(GameObject explosionEffect)
    {

        DestroyNearestEnemyToCameraBottom(explosionEffect);
        scoreManager.AddScore(100);
    }

    public void HandleGameOver()
    {
        SceneManager.LoadScene("DeathScene");
    }

    private void DestroyNearestEnemyToCameraBottom(GameObject explosionEffect)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.LogWarning("�� ����� ��� ������!");
            return;
        }

        Vector3 cameraBottomPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, mainCamera.nearClipPlane));

        GameObject nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Mathf.Abs(enemy.transform.position.y - cameraBottomPosition.y);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
        {
            Vector2 enemyPosition = nearestEnemy.transform.position;

            Destroy(nearestEnemy.gameObject);

            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, (Vector3)enemyPosition, Quaternion.identity);
            }
            else if (defaultExplosionPrefab != null)
            {
                Instantiate(defaultExplosionPrefab, (Vector3)enemyPosition, Quaternion.identity);
            }
            Debug.Log("��������� ��������� ���� � ������ ������� ������.");
        }
        else
        {
            Debug.LogWarning("�� ������� ����� ���������� �����!");
        }
    }
}
