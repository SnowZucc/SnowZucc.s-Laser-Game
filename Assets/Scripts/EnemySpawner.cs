using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public float waveDelay = 5f;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

IEnumerator SpawnWaves()
{
    int waveNumber = 0;

    while (true)
    {
        waveNumber++;
        int enemiesToSpawn = waveNumber <= 5 ? waveNumber : 5;

        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (availableSpawnPoints.Count == 0)
            {
                availableSpawnPoints = new List<Transform>(spawnPoints);
            }

            int spawnPointIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform spawnPoint = availableSpawnPoints[spawnPointIndex];
            availableSpawnPoints.RemoveAt(spawnPointIndex);

            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            StartCoroutine(AddSpawnPointWhenDestroyed(enemy, spawnPoint));

            // Add a delay between each spawn
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitUntil(() => GameObject.FindGameObjectsWithTag("Enemy").Length == 0);
        yield return new WaitForSeconds(waveDelay);
    }
}

IEnumerator AddSpawnPointWhenDestroyed(GameObject enemy, Transform spawnPoint)
{
    yield return new WaitUntil(() => enemy == null);
    spawnPoints.Add(spawnPoint);
}
}