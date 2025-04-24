using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;

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
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        GameEvents.OnEnemyDestroyed += HandleEnemyDestroyed;
        GameEvents.OnEnemyGetAway += HandleEnemyGotAway;
        GameEvents.GameOver += HandleGameOver;
        GameEvents.AddCombo += HandleAddCombo;
        GameEvents.ResetCombo += HandleResetCombo;

        currentStage = stages[0].GetComponent<IStageable>();
        currentStageIndex = 0;

        audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }


    private void OnDestroy()
    {
        GameEvents.OnEnemyDestroyed -= HandleEnemyDestroyed;
        GameEvents.OnEnemyGetAway -= HandleEnemyGotAway;
        GameEvents.GameOver -= HandleGameOver;
        GameEvents.AddCombo -= HandleAddCombo;
        GameEvents.ResetCombo -= HandleResetCombo;
    }
    private void Update()
    {
        ChangeState();
    }

    private void ChangeState()
    {
        if (enemiesKilled == currentStage.EnemiesToKill() && !IsEndlessMode)
        {
            currentStage = stages[currentStageIndex].GetComponent<IStageable>();
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

    public void HandleEnemyGotAway()
    {
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
        currentCombo++;
        UpdateComboText();
    }

    public void HandleResetCombo()
    {
        currentCombo = 1;
        UpdateComboText();
    }

    public void HandleGameOver()
    {
        StopAllCoroutines();
        GameEvents.OnEnemyDestroyed -= HandleEnemyDestroyed;
        GameEvents.OnEnemyGetAway -= HandleEnemyGotAway;
        GameEvents.GameOver -= HandleGameOver;
        GameEvents.AddCombo -= HandleAddCombo;
        GameEvents.ResetCombo -= HandleResetCombo;
        ScoreTransitioner.LastScore = scoreManager.currentScore;
        SceneManager.LoadScene("DeathScene");
    }

    private void UpdateComboText()
    {
        if (comboText != null)
        {
            comboText.text = currentCombo + "x";
        }
    }

    private void DestroyNearestEnemyToCameraBottom()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0) return;

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
            Vector2 pos = nearestEnemy.transform.position;
            Destroy(nearestEnemy);
            GameObject explosion = Instantiate(defaultExplosionPrefab, pos, Quaternion.identity);
            if (explosion != null)
                Destroy(explosion, 1f);
        }
    }
}