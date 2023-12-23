using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class CharacterFight : MonoBehaviour
{
    [SerializeField] CharacterManager characterManager;
    bool isHit = false, isInner = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.gameObject.activeInHierarchy && !isInner)
        {
            isInner = true;
            StartCoroutine(HitTime(other));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { isInner = false; }
    }
    public bool GetIsHit() { return isHit; }

    IEnumerator HitTime(Collider other)
    {
        while (isInner && other.gameObject.activeInHierarchy)
        {
            if (!isHit && other.gameObject.activeInHierarchy)
            {
                isHit = true;
                EnemyManager enemyManager = other.GetComponent<EnemyManager>();
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
