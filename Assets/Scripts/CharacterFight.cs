using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class CharacterFight : MonoBehaviour
{
    [SerializeField] CharacterManager characterManager;
    [SerializeField] GameObject _target;
    bool isHit = false, isInner = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && _target == null)
        {
            _target = other.gameObject;
            EnemyManager enemyManager = other.GetComponent<EnemyManager>();
            if (enemyManager.GetEnemyHealth() > 0)
            {
                isInner = true;
                StartCoroutine(HitTime(enemyManager));
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) if (_target == other.gameObject) { isInner = false; }
    }

    public bool GetIsHit() { return isHit; }

    IEnumerator HitTime(EnemyManager enemyManager)
    {
        while (isInner && enemyManager.GetEnemyHealth() > 0 && _target != null)
        {
            if (!isHit && enemyManager.GetEnemyHealth() > 0 && _target != null)
            {
                isHit = true;
                enemyManager.DownEnemyHeallth(ItemData.Instance.field.characterAttack);
                characterManager.GetAnimController().CallHitAnim();
                yield return new WaitForSeconds(ItemData.Instance.field.characterHitTime);
                isHit = false;
            }
            yield return null; ;
        }
        yield return null; ;
    }
}
