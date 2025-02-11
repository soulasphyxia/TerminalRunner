using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;

    private Camera mainCamera;
    private int lastSpawnedEnemyIndex = -1; // ������ ���������� ���������� �����

    private void Start()
    {
        // �������� ������ �� �������� ������
        mainCamera = Camera.main;
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            // �������� ���������� �����, ��������� �� �����������
            int randomIndex = GetRandomEnemyIndex();
            GameObject enemyToSpawn = enemyPrefabs[randomIndex];

            // ������������ ��������� ������� �� ��� X � �������� ������
            Vector3 spawnPosition = GetRandomSpawnPosition();

            // ������� ����� � ������������ �������
            Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private int GetRandomEnemyIndex()
    {
        int randomIndex;

        // ���������� ��������� ������, ���� �� �� ����� ���������� �� �����������
        do
        {
            randomIndex = Random.Range(0, enemyPrefabs.Length);
        } while (randomIndex == lastSpawnedEnemyIndex);

        // ��������� ������ ���������� ���������� �����
        lastSpawnedEnemyIndex = randomIndex;

        return randomIndex;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // �������� ������� ������ � ������� �����������
        float cameraHeight = mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // ���������� ��������� ������� �� ��� X
        float randomX = Random.Range(-cameraWidth, cameraWidth);

        // ������� �� ��� Y � ������� ���� ������
        float spawnY = cameraHeight;

        // ���������� ������� ������
        return new Vector3(randomX, spawnY, -10);
    }
}