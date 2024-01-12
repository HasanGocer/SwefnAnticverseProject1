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
    List<PickUpSystem> items = new List<PickUpSystem>();
    bool isHit = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            if (IsHere(other.gameObject))
            {
                enemyManager = other.GetComponent<EnemyManager>();
                if (enemyManager.GetEnemyHealth() > 0)
                {
                    enemys.Add(enemyManager);
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
                    items.Add(other.transform.parent.GetComponent<PickUpSystem>());
                    StartCoroutine(Hit());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
            if (!IsHere(other.gameObject))
                enemys.RemoveAt(enemys.IndexOf(other.GetComponent<EnemyManager>()));
        for (int i1 = 0; i1 < items.Count; i1++)
        {
            print(1);
            for (int i = 0; i < TagsManager.Instance.GetTagCount(); i++)
            {
                print(2);
                if (other.CompareTag(TagsManager.Instance.GetTagName(i)))
                {
                    print(3);
                    if (!IsHere(other.gameObject))
                    {
                        print(4);
                        items.RemoveAt(items.IndexOf(other.transform.parent.GetComponent<PickUpSystem>()));

                    }
                }
            }
        }

    }


    public void SetHitBool(bool tempBool)
    {
        isHit = tempBool;
    }
    public bool GetIsHit() { return isHit; }

    private IEnumerator Hit()
    {
        yield return null;
        while ((enemys.Count > 0 || items.Count > 0) && HitChecked())
        {
            yield return null;
            if (!isHit)
            {
                isHit = true;
                yield return null;
                if (LiveCheck())
                    characterManager.GetAnimController().CallHitAnim();
                yield return new WaitForSeconds(0.83f / ItemData.Instance.field.characterHitTime);
                if (LiveCheck())
                    hitCollider.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.17f / ItemData.Instance.field.characterHitTime);
                if (LiveCheck())
                    hitCollider.gameObject.SetActive(false);
                yield return new WaitForSeconds(1.23f / ItemData.Instance.field.characterHitTime);
                if (LiveCheck())
                    if (CharacterManager.Instance.GetAnimController().GetRunAnimBool())
                        characterManager.GetAnimController().SetHitBool(false);
                    else
                        characterManager.GetAnimController().CallIdleAnim();
                LiveEnemyCheck();
                isHit = false;
            }
        }

    }

    private bool LiveCheck()
    {
        if (characterManager.GetCharacterHealth() > 0) return true;
        else return false;
    }
    private bool HitChecked()
    {
        bool tempBool = false;

        for (int i = 0; i < items.Count; i++)
            if (items[i].GetIsOpen()) tempBool = true;

        for (int i = 0; i < enemys.Count; i++)
            if (enemys[i].GetEnemyHealth() <= 0) tempBool = true;
        return tempBool;
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
            if (enemys[i].gameObject == tempObject) tempBool = false;
        for (int i = 0; i < items.Count; i++)
            if (items[i].gameObject == tempObject) tempBool = false;
        return tempBool;
    }
}
