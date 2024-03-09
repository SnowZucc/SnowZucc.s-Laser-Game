using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public float waveDelay = 3f;

    private int killCount = 0; // The current kill count
    private int waveNumber = 0; // The current wave number

    public GameObject overlay;
    public GameObject overlayChild;
    private TextMeshProUGUI overlayText;
    public TextMeshPro gameInfoText; // The TextMeshPro that displays the game info



    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            waveNumber++;
            UpdateGameInfoText();
            StartCoroutine(ShowOverlay());
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
        IncrementKillCount();
    }




    public void UpdateWaveNumber(int newWaveNumber)
    {
        waveNumber = newWaveNumber;
        UpdateGameInfoText();
    }

    public void IncrementKillCount()
    {
        killCount++;
        UpdateGameInfoText();
    }

    private void UpdateGameInfoText()
    {
        gameInfoText.text = "Wave: " + waveNumber + "\nKills: " + killCount;
    }

    void Update()
    {
        UpdateGameInfoText();
    }

    private IEnumerator ShowOverlay()
    {
        overlayText = overlayChild.GetComponent<TextMeshProUGUI>();
        overlayText.text = "Wave: " + waveNumber + "\nKills: " + killCount;
        overlay.SetActive(true);
        yield return new WaitForSeconds(2);
        overlay.SetActive(false);
    }
}