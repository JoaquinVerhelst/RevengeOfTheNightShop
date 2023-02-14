using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject enemyObject;
    Transform targetTransform;


    public void SpawnEnemy()
    {
        EnemyBehaviour enemyBehavior = enemyObject.GetComponent<EnemyBehaviour>();
        enemyBehavior.SetTarget(targetTransform);
        Instantiate(enemyObject, transform);

    }

    public void SetTarget(Transform target)
    {
        targetTransform= target;
    }

    public void SetEnemyObject(GameObject enemy)
    {
        enemyObject = enemy;
    }

}
