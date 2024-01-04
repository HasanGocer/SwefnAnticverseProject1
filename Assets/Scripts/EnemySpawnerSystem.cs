using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSystem : MonoBehaviour
{
    [SerializeField] int enemySpawnerCount;
    [SerializeField] List<GameObject> enemySpawnPlace;
    [SerializeField] List<bool> enemySpawnBool;
    [SerializeField] List<GameObject> enemyObjects;

    private void Start()
    {
        StartSpawner();
    }

    void Update()
    {
        if (enemySpawnerCount < EnemySpawnerManager.Instance.GetSpawnerEnemyCount())
        {
            enemySpawnerCount++;
            StartCoroutine(SpawnerAgainStart());
        }
    }

    public void EnemyDeath(GameObject deathEnemy)
    {
        int tempDeathEnemyCount = 0;

        for (int i = 0; i < EnemySpawnerManager.Instance.GetSpawnerEnemyCount(); i++)
            if (enemyObjects[i] == deathEnemy)
            {
                tempDeathEnemyCount = i;
                break;
            }
        enemySpawnerCount--;
        enemySpawnBool[tempDeathEnemyCount] = false;

    }

    IEnumerator SpawnerAgainStart()
    {
        yield return new WaitForSeconds(EnemySpawnerManager.Instance.GetSpawnerEnemyCountDown());

        for (int i = 0; i < EnemySpawnerManager.Instance.GetSpawnerEnemyCount(); i++)
            if (!enemySpawnBool[i])
            {
                EnemyAgainStartSpawn(i);
            }
    }

    void StartSpawner()
    {
        for (int i = 0; i < EnemySpawnerManager.Instance.GetSpawnerEnemyCount(); i++)
            EnemyStartSpawn(i);
    }

    void EnemyStartSpawn(int enemyCount)
    {
        GameObject tempObject = ObjectPool.Instance.GetPooledObject(EnemySpawnerManager.Instance.GetSpawnerEnemyCountOP(), enemySpawnPlace[enemyCount].transform.position);
        enemyObjects.Add(tempObject);
        enemySpawnBool.Add(true);
        enemySpawnerCount++;
        tempObject.GetComponent<EnemyManager>().SetEnemySpawnerSystem(this);
    }

    void EnemyAgainStartSpawn(int enemyCount)
    {
        GameObject tempObject = ObjectPool.Instance.GetPooledObject(EnemySpawnerManager.Instance.GetSpawnerEnemyCountOP(), enemySpawnPlace[enemyCount].transform.position);
        enemyObjects[enemyCount] = tempObject;
        enemySpawnBool[enemyCount] = true;
        tempObject.GetComponent<EnemyManager>().SetEnemySpawnerSystem(this);
    }

}
