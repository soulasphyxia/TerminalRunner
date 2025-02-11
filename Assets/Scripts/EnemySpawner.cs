using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;

    private Camera mainCamera;
    private int lastSpawnedEnemyIndex = -1; // Индекс последнего созданного врага

    private void Start()
    {
        // Получаем ссылку на основную камеру
        mainCamera = Camera.main;
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            // Выбираем случайного врага, отличного от предыдущего
            int randomIndex = GetRandomEnemyIndex();
            GameObject enemyToSpawn = enemyPrefabs[randomIndex];

            // Рассчитываем случайную позицию по оси X в пределах экрана
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // Создаем врага в рассчитанной позиции
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
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