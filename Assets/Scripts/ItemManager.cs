using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoSingleton<ItemManager>
{
    [System.Serializable]
    public class ItemDatas
    {
        public List<int> itemCount = new List<int>();
        public List<float> itemReloadTime = new List<float>();
    }

    [SerializeField] private ItemDatas items;

    public int GetItemCount(int tagCount)
    {
        return items.itemCount[tagCount];
    }
    public float GetItemReloadTime(int tagCount)
    {
        return items.itemReloadTime[tagCount];
    }
}
