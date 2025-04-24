using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float spawnRate = 2f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = false;
    private float enemiesSpeed = 0.5f;
    [SerializeField] List<GameObject> enemies = new();
    [SerializeField] Transform container;

    private Camera mainCamera;
    private int lastSpawnedEnemyIndex = -1;


    public float SpawnRate
    {
        get { return spawnRate; }
        set { spawnRate = value; }
    }

    public float EnemiesSpeed
    {
        get { return enemiesSpeed; }
        set { enemiesSpeed = value; }
    }

    public bool CanSpawn
    {
        get { return canSpawn; }
        set { canSpawn = value; }
    }

    public GameObject RemoveFirstEnemy()
    {
        GameObject enemy = enemies[0];
        enemies.RemoveAt(0);
        return enemy;
    }

    public List<GameObject> Enemies
    {
        get { return enemies; }
    }

    public void Awake()
    {
        GameEvents.OnChangeStage += (stage) => HandleStageChanged(stage);
        this.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ClearAllEnemies();
        canSpawn = false;
        container.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        container.gameObject.SetActive(true);
        canSpawn = true;
        mainCamera = Camera.main;
        enemies.Clear();
        StartCoroutine(Spawner());
    }

    private void HandleStageChanged(IStageable stage)
    {
        spawnRate = stage.GetSpawnRate();
        enemiesSpeed = stage.GetSpeed();
        StartCoroutine(DisableComponentCoroutine(3.5f));
    }

    private System.Collections.IEnumerator DisableComponentCoroutine(float seconds)
    {
        
        // Отключаем компонент
        this.enabled = false;

        // Ждем указанное количество секунд
        yield return new WaitForSeconds(seconds);

        // Включаем компонент обратно
        this.enabled = true;
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn)
        {
            SpawnEnemy();
            yield return wait;
        }
    }

    public void SpawnEnemy()
    {
        int randomIndex = GetRandomEnemyIndex();
        GameObject enemyToSpawn = enemyPrefabs[randomIndex];

        Vector3 spawnPosition = GetRandomSpawnPosition();

        GameObject enemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity, container);
        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        enemyMovement.Speed = enemiesSpeed;
    }

    public void ClearAllEnemies()
    {
        if (container != null)
        {
            // Удаляем всех дочерних объектов контейнера
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }

            Debug.Log("Все враги очищены.");
        }
        else
        {
            Debug.LogWarning("Контейнер для врагов не назначен!");
        }
    }

    private int GetRandomEnemyIndex()
    {
        int randomIndex;

        // Генерируем случайный индекс, пока он не будет отличаться от предыдущего
        do
        {
            randomIndex = Random.Range(0, enemyPrefabs.Length);
        } while (randomIndex == lastSpawnedEnemyIndex);

        // Обновляем индекс последнего созданного врага
        lastSpawnedEnemyIndex = randomIndex;

        return randomIndex;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Получаем границы экрана в мировых координатах
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Генерируем случайную позицию по оси X
        float randomX = Random.Range(-cameraWidth, cameraWidth);

        // Позиция по оси Y — верхний край экрана
        float spawnY = cameraHeight;

        // Возвращаем позицию спавна
        return new Vector3(randomX, spawnY, -10);
    }
}