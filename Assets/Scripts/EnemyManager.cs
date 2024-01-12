using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Coroutine healthCorotine;
    [SerializeField] EnemyFightSystem enemyFightSystem;
    [SerializeField] EnemySpawnerSystem enemySpawnerSystem;
    [SerializeField] EnemyBar enemyBar;
    [SerializeField] int health;
    [SerializeField] int enemyCount;
    [SerializeField] bool isLive = true, systemReset = false;

    private void OnEnable()
    {
        health = EnemyFightManager.Instance.GetEnemyHealth(enemyCount);
    }

    public bool GetIsLive() { return isLive; }
    public void SetIsLive(bool tempIsLive) { isLive = tempIsLive; }
    public int GetEnemyHealth() { return health; }
    public void EnemyHealthReHealth() { health = EnemyFightManager.Instance.GetEnemyHealth(enemyCount); }
    public int GetEnemyCount() { return enemyCount; }
    public void SetEnemySpawnerSystem(EnemySpawnerSystem tempEnemySpawnerSystem) { enemySpawnerSystem = tempEnemySpawnerSystem; }
    public void DownEnemyHeallth(int tempHealth)
    {
        health -= tempHealth;
        CheckHealth();
        if (!systemReset)
            healthCorotine = StartCoroutine(HealthUp());
        else
        {
            StopCoroutine(healthCorotine);
            healthCorotine = StartCoroutine(HealthUp());
        }
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {

            SetIsLive(false);
            enemyFightSystem.GetEnemyAnim().CallDeathAnim();
            StartCoroutine(EnemyFightManager.Instance.DeathThrowCoin(enemyCount, gameObject));
            StartCoroutine(DeathTime());
            enemySpawnerSystem.EnemyDeath(gameObject);
        }
    }

    IEnumerator HealthUp()
    {
        systemReset = true;
        yield return new WaitForSeconds(EnemyFightManager.Instance.GetEnemyReHealthCountdownContdown());
        EnemyHealthReHealth();
        systemReset = false;
    }

    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(EnemyFightManager.Instance.GetDeathTime());
        gameObject.SetActive(false);
    }
}
