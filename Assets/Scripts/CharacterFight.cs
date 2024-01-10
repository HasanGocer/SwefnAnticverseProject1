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
    List<EnemyManager> enemys = new List<EnemyManager>();
    List<GameObject> items = new List<GameObject>();
    bool isHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isHit)
        {
            if (other.CompareTag("Enemy"))
                if (IsHere(other.gameObject))
                {
                    enemyManager = other.GetComponent<EnemyManager>();
                    if (enemyManager.GetEnemyHealth() > 0)
                    {
                        enemys.Add(enemyManager);
                        isHit = true;
                        StartCoroutine(Hit());
                    }
                }
            for (int i = 0; i < TagsManager.Instance.GetTagCount(); i++)
            {
                print(1);
                if (other.CompareTag(TagsManager.Instance.GetTagName(i)))
                {
                    print(2);
                    if (IsHere(other.gameObject))
                    {
                        print(3);
                        items.Add(other.gameObject);
                        isHit = true;
                        StartCoroutine(Hit());
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            if (!IsHere(other.gameObject))
                enemys.RemoveAt(enemys.IndexOf(other.GetComponent<EnemyManager>()));

        for (int i = 0; i < TagsManager.Instance.GetTagCount(); i++)
            if (other.CompareTag(TagsManager.Instance.GetTagName(i)))
                if (!IsHere(other.gameObject))
                    items.RemoveAt(items.IndexOf(other.gameObject));
    }


    public void SetHitBool(bool tempBool)
    {
        isHit = tempBool;
    }
    public bool GetIsHit() { return isHit; }

    private IEnumerator Hit()
    {
        yield return null;
        print(31);
        while (enemys.Count > 0 || items.Count > 0)
        {
            yield return null;
            if (isHit)
            {
                print(4);
                yield return null;
                if (LiveCheck())
                    characterManager.GetAnimController().CallHitAnim();
                print(5);
                yield return new WaitForSeconds(0.83f / ItemData.Instance.field.characterHitTime);
                print(6);
                if (LiveCheck())
                    hitCollider.gameObject.SetActive(true);
                print(7);
                yield return new WaitForSeconds(0.17f / ItemData.Instance.field.characterHitTime);
                if (LiveCheck())
                    hitCollider.gameObject.SetActive(false);
                print(8);
                yield return new WaitForSeconds(1.23f / ItemData.Instance.field.characterHitTime);
                if (LiveCheck())
                    if (CharacterManager.Instance.GetAnimController().GetRunAnimBool())
                        characterManager.GetAnimController().SetHitBool(false);
                    else
                        characterManager.GetAnimController().CallIdleAnim();
                LiveEnemyCheck();
            }
        }

    }

    private bool LiveCheck()
    {
        if (characterManager.GetCharacterHealth() > 0) return true;
        else return false;
    }
    private void LiveEnemyCheck()
    {
        for (int i = 0; i < enemys.Count;)
        {
            if (enemys[i].GetEnemyHealth() <= 0) enemys.RemoveAt(i);
            else i++;
        }
    }
    private bool IsHere(GameObject tempObject)
    {
        bool tempBool = true;

        for (int i = 0; i < enemys.Count; i++)
            if (enemys[i] == tempObject) tempBool = false;
        return tempBool;
    }
}
