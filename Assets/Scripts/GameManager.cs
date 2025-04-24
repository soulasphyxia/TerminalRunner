using System.Collections.Generic;
using TMPro;
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

    public TMP_Text comboText;

    private IStageable currentStage;
    private int currentStageIndex;

    private AudioSource[] audioSources;
    private bool IsEndlessMode = false;

    private int enemiesKilled = 0;

    private int currentCombo = 1;


    private void Awake()
    {
        DontDestroyOnLoad(mainCamera);
        GameEvents.OnEnemyDestroyed += HandleEnemyDestroyed;
        GameEvents.OnEnemyGetAway += HandleEnemyGotAway;
        GameEvents.GameOver += HandleGameOver;
        GameEvents.AddCombo += HandleAddCombo;
        GameEvents.ResetCombo += HandleResetCombo;
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

    public void HandleEnemyGotAway() {
        lifeManager.LoseLife();
    }

    public void HandleEnemyDestroyed()
    {
        DestroyNearestEnemyToCameraBottom();
        scoreManager.AddScore(100 * currentCombo);
        enemiesKilled++;
    }

    public void HandleAddCombo()
    {
        currentCombo += 1;
        UpdateComboText();
    }

    public void HandleResetCombo()
    {
        currentCombo = 1;
        UpdateComboText();
    }

    public void HandleGameOver()
    {
        SceneManager.LoadScene("DeathScene");
    }

    private void UpdateComboText()
    {
        if (comboText != null)
        {
            comboText.text = currentCombo.ToString() + "x";
        }
    }

    private void DestroyNearestEnemyToCameraBottom()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.LogWarning("На сцене нет врагов!");
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
            explosion = Instantiate(defaultExplosionPrefab, (Vector3)enemyPosition, Quaternion.identity);
            
            Debug.Log("Уничтожен ближайший враг к нижней границе камеры.");
            if (explosion != null) {
                Destroy(explosion, 1f);
            }
        }
        else
        {
            Debug.LogWarning("Не удалось найти ближайшего врага!");
        }
    }
}
