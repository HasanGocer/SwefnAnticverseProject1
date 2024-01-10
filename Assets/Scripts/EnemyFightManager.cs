using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFightManager : MonoSingleton<EnemyFightManager>
{
    [SerializeField] List<int> enemyHealthes = new List<int>();
    [SerializeField] List<int> enemyAttackPower = new List<int>();
    [SerializeField] List<int> enemyAttackCountDown = new List<int>();
    [SerializeField] int minViewDistance;
    [SerializeField] int minHitDistance;
    [SerializeField] float walkSpeed;
    [SerializeField] int deathTime;
    [SerializeField] int deathThrowCoinOP;
    [SerializeField] float deathThrowCoinWaitTime;
    public int GetEnemyHealth(int enemyCount) { return enemyHealthes[enemyCount]; }
    public int GetEnemyAttackPower(int enemyCount) { return enemyAttackPower[enemyCount]; }
    public int GetEnemyAttackCountDown(int enemyCount) { return enemyAttackCountDown[enemyCount]; }
    public int GetMinViewDistance() { return minViewDistance; }
    public int GetMinHitDistance() { return minHitDistance; }
    public float GetWalkSpeed() { return walkSpeed; }
    public int GetDeathTime() { return deathTime; }
    public int GetDeathThrowCoinOP() { return deathThrowCoinOP; }
    public float GetDeathThrowCoinWaitTime() { return deathThrowCoinWaitTime; }

    public IEnumerator DeathThrowCoin(int enemyCount, GameObject enemy)
    {
        StartCoroutine(ThrowItemSystem.Instance.LaunchRandomItems((enemyCount + 1) * 5, deathThrowCoinOP, enemy,enemy));
        yield return new WaitForSeconds(deathThrowCoinWaitTime);
    }
}
