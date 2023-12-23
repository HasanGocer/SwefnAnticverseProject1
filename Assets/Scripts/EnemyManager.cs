using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] EnemyFightSystem enemyFightSystem;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] EnemySpawnerSystem enemySpawnerSystem;
    [SerializeField] int health;
    [SerializeField] int enemyCount;
    [SerializeField] bool isLive = true;

    private void OnEnable()
    {
        health = EnemyFightManager.Instance.GetEnemyHealth(enemyCount);
    }

    public bool GetIsLive() { return isLive; }
    public void SetIsLive(bool tempIsLive) { isLive = tempIsLive; }
    public int GetEnemyHealth() { return health; }
    public int GetEnemyCount() { return enemyCount; }
    public void SetEnemySpawnerSystem(EnemySpawnerSystem tempEnemySpawnerSystem) { enemySpawnerSystem = tempEnemySpawnerSystem; }
    public void DownEnemyHeallth(int tempHealth)
    {
        health -= tempHealth;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {

            enemyManager.SetIsLive(false);
            enemyFightSystem.GetEnemyAnim().CallDeathAnim();
            StartCoroutine(EnemyFightManager.Instance.DeathThrowCoin(enemyCount, gameObject));
            StartCoroutine(DeathTime());
            enemySpawnerSystem.EnemyDeath(gameObject);
        }
    }



    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(EnemyFightManager.Instance.GetDeathTime());
        gameObject.SetActive(false);
    }
}
