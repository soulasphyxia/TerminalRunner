using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using NUnit.Framework.Internal.Filters;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] LifeManager lifeManager;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject defaultExplosionPrefab;
    [SerializeField] private TextHandlerManager textHandlerManager;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject[] stages;
    private Queue<IStageable> stagesQueue;

    private IStageable currentStage;
    private int currentStageIndex;

    private AudioSource[] audioSources;
    private bool IsEndlessMode = false;

    private int enemiesKilled = 0;


    private void Awake()
    {
        GameEvents.OnEnemyDestroyed += (enemy) => HandleEnemyDestroyed(enemy);
        GameEvents.OnEnemyGetAway += HandleEnemyGotAway;
        GameEvents.GameOver += HandleGameOver;
        audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        currentStage = stages[0].GetComponent<IStageable>();
        currentStageIndex = 0;
    }

    public void Update()
    {
        ChangeState();
    }


    private void ChangeState()
    {
        if (enemiesKilled == currentStage.EnemiesToKill() && !IsEndlessMode)
        {
            currentStage = stages[currentStageIndex].GetComponent<IStageable>();
            Debug.Log($"{currentStage.GetStageNumber()}, {currentStage.IsLastStage()}");
            GameEvents.CallChangeStage(currentStage);
            enemiesKilled = 0;
            currentStageIndex++;
        }
        if (currentStage.IsLastStage())
        {
            IsEndlessMode = true;
        }
    }

    public void StartGame()
    {
        textHandlerManager.gameObject.SetActive(true);
        textHandlerManager.OnDisplayText();
        enemySpawner.gameObject.SetActive(true);
        enemySpawner.CanSpawn = true;
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.Play();
            }
        }
    }

    public void StopGame()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        textHandlerManager.gameObject.SetActive(false);
        enemySpawner.gameObject.SetActive(false);
        textHandlerManager.OffDisplayText();
        enemySpawner.CanSpawn = false;
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.Stop();
            }
        }
    }

    public void HandleEnemyGotAway() {
        lifeManager.LoseLife();
    }

    public void HandleEnemyDestroyed(GameObject explosionEffect)
    {
        DestroyNearestEnemyToCameraBottom(explosionEffect);
        scoreManager.AddScore(100);
        enemiesKilled++;
    }

    public void HandleGameOver()
    {
        ScoreTransitioner.LastScore = scoreManager.currentScore; // Сохраняем очки
        StopGame();
        SceneManager.LoadScene("DeathScene");
    }

    private void DestroyNearestEnemyToCameraBottom(GameObject explosionEffect)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.LogWarning("Íà ñöåíå íåò âðàãîâ!");
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

            GameObject explosion = null;

            if (explosionEffect != null)
            {
                explosion = Instantiate(explosionEffect, (Vector3)enemyPosition, Quaternion.identity);
            }
            else if (defaultExplosionPrefab != null)
            {
                explosion = Instantiate(defaultExplosionPrefab, (Vector3)enemyPosition, Quaternion.identity);
            }
            Debug.Log("Óíè÷òîæåí áëèæàéøèé âðàã ê íèæíåé ãðàíèöå êàìåðû.");
            if (explosion != null) {
                Destroy(explosion, 1f);
            }
        }
        else
        {
            Debug.LogWarning("Íå óäàëîñü íàéòè áëèæàéøåãî âðàãà!");
        }
    }
}
