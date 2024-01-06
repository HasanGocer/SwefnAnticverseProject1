using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{

    [SerializeField] private int tagCount;
    [SerializeField] private int itemCount;
    [SerializeField] List<GameObject> Levels = new List<GameObject>();
    bool hitTime = false, systemReset = false;

    private void OnEnable()
    {
        itemCount = ItemManager.Instance.GetItemCount(tagCount);
        ChangeItemAppearance();
    }

    private void _ColliderTouchStart()
    {
        hitTime = true;
        StartCoroutine(HitTime());
    }
    private void _ColliderTouchStop()
    {
        hitTime = false;
        //CharacterManager.Instance.GetAnimController().CallIdleAnim();
    }
    private void Update()
    {
        /*if (hitTime)
            if (itemCount == 0)
                CharacterManager.Instance.GetAnimController().CallIdleAnim();*/
    }
    public void ColliderTouchStop()
    {
        _ColliderTouchStop();
    }
    public void ColliderTouchStart()
    {
        _ColliderTouchStart();
    }

    IEnumerator ItemReloadIenum()
    {
        systemReset = true;
        yield return new WaitForSeconds(ItemManager.Instance.GetItemReloadTime(tagCount));
        itemCount = ItemManager.Instance.GetItemCount(tagCount);
        ChangeItemAppearance();
        systemReset = false;
    }
    IEnumerator HitTime()
    {
        while (hitTime && itemCount > 0)
        {
            print(1);
            CharacterManager.Instance.GetAnimController().CallHitAnim();
            yield return new WaitForSeconds(ItemData.Instance.field.HitTime[tagCount]);
            TagsManager.Instance.AddTagCount(tagCount, ItemData.Instance.field.itemHitCount[tagCount]);
            itemCountDown(ItemData.Instance.field.itemHitCount[tagCount]);
            if (hitTime)
            {
                ChangeItemAppearance();
                StartCoroutine(ThrowItemSystem.Instance.LaunchRandomItems(ItemData.Instance.field.itemHitCount[tagCount], tagCount, gameObject));
                if (itemCount != ItemManager.Instance.GetItemCount(tagCount)) if (!systemReset) StartCoroutine(ItemReloadIenum());
            }
        }
        yield return null;
    }
    private void itemCountDown(int downCount)
    {
        itemCount -= downCount;
    }

    private void ChangeItemAppearance()
    {
        int levelCount = (int)((float)((float)itemCount / (float)ItemManager.Instance.GetItemCount(tagCount)) * (float)Levels.Count) - 1;
        if (levelCount < 0) levelCount = 0;
        LevelOff();
        Levels[levelCount].SetActive(true);
    }

    private void LevelOff()
    {
        for (int i = 0; i < Levels.Count; i++)
            Levels[i].SetActive(false);
    }


}
