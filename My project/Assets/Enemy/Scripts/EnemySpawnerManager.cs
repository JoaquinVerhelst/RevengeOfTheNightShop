using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] float minTimeToSpawn = 10;
    [SerializeField] float maxTimeBetweenCycle = 6;
    private float randomSpawnTime;
    [SerializeField] Transform target;
    int currentIndex;
    EnemySpawner[] enemySpawners;
    private float spawnTimer;

    public void Awake()
    {
        enemySpawners = GetComponentsInChildren<EnemySpawner>();

        SetSpawnersTarget();
        SetSpawnersEnemyObject();

        spawnTimer = 10;
        randomSpawnTime = minTimeToSpawn;
    }

    public void Update()
    {
        spawnTimer += Time.deltaTime;
        SpawnEnemies();
        
    }


    public void SetSpawnersTarget()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].SetTarget(target);
        }
    }
    public void SetSpawnersEnemyObject()
    {
        for (int i = 0; i < enemySpawners.Length; i++)
        {
            enemySpawners[i].SetEnemyObject(enemyObject);
        }
    }

    public void SpawnEnemies()
    {
        if (spawnTimer >= randomSpawnTime)
        {
            enemySpawners[currentIndex].SpawnEnemy();

            randomSpawnTime += Random.Range(1, maxTimeBetweenCycle);

            currentIndex++;

            if (currentIndex >= enemySpawners.Length)
            {
                currentIndex = 0;
                randomSpawnTime = minTimeToSpawn;
                spawnTimer = 0;

            }
        }
    }


    public void RandomSpawnTime()
    {
        //float timeleft = maxTimeToSpawn - minTimeToSpawn;





    }
}
