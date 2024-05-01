using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    //managerde bulunacak

    public enum GameStat
    {
        intro = 0,
        start = 1,
        wait = 2,
        finish = 3
    }


    [Header("Game_Main_Field")]
    [Space(10)]

    public GameStat gameStat;
    public int addedMoney;
    public int level;
    public int money;
    public int vibration;
    public int sound;
    public int music;

    public void Awake()
    {
        PlayerPrefsPlacement();
        ItemData.Instance.AwakeID();
    }

    private void PlayerPrefsPlacement()
    {
        if (PlayerPrefs.HasKey("money"))
            money = PlayerPrefs.GetInt("money");
        else
            PlayerPrefs.SetInt("money", 100);
        MoneySystem.Instance.MoneyTextRevork(0);

        if (PlayerPrefs.HasKey("level"))
            level = PlayerPrefs.GetInt("level");
        else
            PlayerPrefs.SetInt("level", level);

        if (PlayerPrefs.HasKey("vibration"))
            vibration = PlayerPrefs.GetInt("vibration");
        else
            PlayerPrefs.SetInt("vibration", vibration);

        if (PlayerPrefs.HasKey("sound"))
            vibration = PlayerPrefs.GetInt("sound");
        else
            PlayerPrefs.SetInt("sound", sound);

        if (PlayerPrefs.HasKey("music"))
            vibration = PlayerPrefs.GetInt("music");
        else
            PlayerPrefs.SetInt("music", music);

        if (PlayerPrefs.HasKey("first"))
        {
            ItemManager.Instance.StartWrite(ItemsPlacementRead());
            ItemData.Instance.factor = FactorPlacementRead();
        }
        else
        {
            PlayerPrefs.SetInt("first", 1);
            ItemManager.Instance.StartWrite(ItemManager.Instance.GetItemDatas());
            FactorPlacementWrite(ItemData.Instance.factor);
        }
    }

    public void FactorPlacementWrite(ItemData.Field factor)
    {
        string jsonData = JsonUtility.ToJson(factor);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/FactorData.json", jsonData);
    }
    public void ItemsPlacementWrite(ItemManager.ItemDatas items)
    {
        string jsonData = JsonUtility.ToJson(items);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/ItemData.json", jsonData);
    }

    public ItemManager.ItemDatas ItemsPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/ItemData.json");
        ItemManager.ItemDatas items = new ItemManager.ItemDatas();
        items = JsonUtility.FromJson<ItemManager.ItemDatas>(jsonRead);
        return items;
    }
    public ItemData.Field FactorPlacementRead()
    {
        string jsonRead = System.IO.File.ReadAllText(Application.persistentDataPath + "/FactorData.json");
        ItemData.Field factor = new ItemData.Field();
        factor = JsonUtility.FromJson<ItemData.Field>(jsonRead);
        return factor;
    }

    public void SetMoney()
    {
        PlayerPrefs.SetInt("money", money);
    }

    public void SetLevel()
    {
        level++;
        PlayerPrefs.SetInt("level", level);
    }

    public void SetVibration()
    {
        PlayerPrefs.SetInt("vibration", vibration);
    }

    public void SetSound()
    {
        PlayerPrefs.SetInt("sound", sound);
    }
    public void SetMusic()
    {
        PlayerPrefs.SetInt("music", music);
    }
}
