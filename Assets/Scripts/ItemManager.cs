using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemManager : MonoSingleton<ItemManager>
{
    [System.Serializable]
    public class ItemDatas
    {
        public List<int> itemUICount = new List<int>();
        public List<int> itemCount = new List<int>();
        public List<float> itemReloadTime = new List<float>();
    }

    [SerializeField] private List<TMP_Text> itemTextCount = new List<TMP_Text>();
    [SerializeField] TMP_Text coinTextCount;

    [SerializeField] private ItemDatas items;

    public int GetItemCount(int tagCount)
    {
        return items.itemCount[tagCount];
    }
    public float GetItemReloadTime(int tagCount)
    {
        return items.itemReloadTime[tagCount];
    }
    public ItemDatas GetItemDatas()
    {
        return items;
    }
    public void ItemCountTextPlacement()
    {
        for (int i = 0; i < itemTextCount.Count; i++)
            itemTextCount[i].text = items.itemUICount[i].ToString();
    }
    public void UPItemUICount(int tagCount, int itemCount)
    {
        items.itemUICount[tagCount] += itemCount;
        itemTextCount[tagCount].text = items.itemUICount[tagCount].ToString();
    }
    public void UpCoinUICount(int itemCount)
    {
        GameManager.Instance.money += itemCount;
        GameManager.Instance.SetMoney();
        coinTextCount.text = GameManager.Instance.money.ToString();
    }
    public void ReWriteItemCount()
    {
        GameManager.Instance.ItemsPlacementWrite(items);
    }
    public void StartWrite(ItemDatas tempItems)
    {
        items = tempItems;
    }
}
