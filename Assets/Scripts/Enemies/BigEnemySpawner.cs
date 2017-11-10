using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BigEnemySpawner : NetworkBehaviour
{
    public Transform target;

    public GameObject enemyPrefab;

    public Transform spawnPoint;

    public float timeBetweenWaves = 10f;
    private float countdown = 4f;

    public int waveIndex = 0;

    void Update()
    {
        // Starts the spawn wave function if the countdown is 0
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        // Setting countdown as time.deltatime
        countdown -= Time.deltaTime;

        // Setting clamps for countdown
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave()
    {
        // Everytime it loops through this function, the waveindex is increased by 1
        waveIndex++;

        // Clamping the max wave index
        waveIndex = Mathf.Clamp(waveIndex, 0, 5);

        // For each wave index, SpawnEnemy will be called according to the number of waveindex
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();

            // spawn an enemy at 1 second intervals
            yield return new WaitForSeconds(8f);
        }
    }

    // Spawns the enemy
    void SpawnEnemy()
    {
        GameObject clone = Instantiate(enemyPrefab, spawnPoint.position, transform.rotation);
        NetworkServer.Spawn(clone);
    }
}
