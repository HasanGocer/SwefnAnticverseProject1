using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoSingleton<EnemySpawnerManager>
{
    [SerializeField] int spawnerEnemyCount;
    [SerializeField] int spawnerEnemyCountDown;
    [SerializeField] int spawnerEnemyCountOP;

    public int GetSpawnerEnemyCount() { return spawnerEnemyCount; }
    public int GetSpawnerEnemyCountDown() { return spawnerEnemyCountDown; }
    public int GetSpawnerEnemyCountOP() { return spawnerEnemyCountOP; }

}
