using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] EnemyManager enemyManager;
    bool isHit = false;
    private void OnDisable()
    {
        isHit = false;
    }
    public bool GetIsHit()
    { return isHit; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main") && !isHit)
        {
            isHit = true;
            CharacterManager.Instance.DownHealth(EnemyFightManager.Instance.GetEnemyAttackPower(enemyManager.GetEnemyCount()));
        }
    }
}
