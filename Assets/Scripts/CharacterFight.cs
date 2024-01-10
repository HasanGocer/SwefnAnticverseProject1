using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class CharacterFight : MonoBehaviour
{
    [SerializeField] CharacterManager characterManager;
    [SerializeField] Collider hitCollider;
    [SerializeField] EnemyManager enemyManager;
    bool isHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isHit)
            if (other.CompareTag("Enemy"))
            {
                enemyManager = other.GetComponent<EnemyManager>();
                if (enemyManager.GetEnemyHealth() > 0)
                {
                    isHit = true;
                    StartCoroutine(Hit());
                }
            }

    }

    public void SetHitBool(bool tempBool)
    {
        isHit = tempBool;
    }
    public bool GetIsHit() { return isHit; }

    public IEnumerator Hit()
    {
        while (isHit && enemyManager.GetEnemyHealth() > 0)
        {
            yield return null;
            if (LiveCheck())
                characterManager.GetAnimController().CallHitAnim();
            yield return new WaitForSeconds(0.83f / ItemData.Instance.field.characterHitTime);
            if (LiveCheck())
                hitCollider.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.17f / ItemData.Instance.field.characterHitTime);
            if (LiveCheck())
                hitCollider.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.23f / ItemData.Instance.field.characterHitTime);
            if (LiveCheck())
                if (CharacterManager.Instance.GetAnimController().GetRunAnimBool())
                    CharacterManager.Instance.GetAnimController().SetHitBool(false);
                else
                    characterManager.GetAnimController().CallIdleAnim();
            if (LiveCheck())
                hitCollider.gameObject.SetActive(false);
            if (LiveCheck())
                CharacterManager.Instance.GetAnimController().SetHitBool(false);
        }
        if (enemyManager != null)
            if (enemyManager.GetEnemyHealth() <= 0)
                isHit = false;
        yield return null;
    }

    private bool LiveCheck()
    {
        if (characterManager.GetCharacterHealth() > 0) return true;
        else return false;
    }

}
