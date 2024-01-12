using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{

    [SerializeField] private int tagCount;
    [SerializeField] private int itemCount;
    [SerializeField] List<GameObject> Levels = new List<GameObject>();
    [SerializeField] GameObject pushObjectStartPos;
    bool systemReset = false, isOpen = false;

    private void OnEnable()
    {
        itemCount = ItemManager.Instance.GetItemCount(tagCount);
        ChangeItemAppearance();
    }
    private void Update()
    {
        if (itemCount > 0) isOpen = true;
        else isOpen = false;
    }

    public bool GetIsOpen() { return isOpen; }

    public void HitFinish()
    {
        if (itemCount > 0)
        {
            TagsManager.Instance.AddTagCount(tagCount, ItemData.Instance.field.itemHitCount[tagCount]);
            itemCountDown(ItemData.Instance.field.itemHitCount[tagCount]);
            StartCoroutine(ThrowItemSystem.Instance.LaunchRandomItems(ItemData.Instance.field.itemHitCount[tagCount], tagCount, gameObject, pushObjectStartPos));
            ChangeItemAppearance();
            if (itemCount != ItemManager.Instance.GetItemCount(tagCount)) if (!systemReset) StartCoroutine(ItemReloadIenum());
        }
    }

    IEnumerator ItemReloadIenum()
    {
        systemReset = true;
        yield return new WaitForSeconds(ItemManager.Instance.GetItemReloadTime(tagCount));
        itemCount = ItemManager.Instance.GetItemCount(tagCount);
        ChangeItemAppearance();
        systemReset = false;
    }
    private void itemCountDown(int downCount)
    {
        itemCount -= downCount;
    }

    private void ChangeItemAppearance()
    {
        int levelCount = (int)((float)((float)itemCount / (float)ItemManager.Instance.GetItemCount(tagCount)) * (float)Levels.Count);
        if (levelCount == Levels.Count) levelCount = Levels.Count - 1;
        LevelOff();
        Levels[levelCount].SetActive(true);
    }

    private void LevelOff()
    {
        for (int i = 0; i < Levels.Count; i++)
            Levels[i].SetActive(false);
    }


}
