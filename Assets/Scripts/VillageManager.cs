using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageManager : MonoSingleton<VillageManager>
{
    [SerializeField] List<GameObject> villagePanel = new List<GameObject>();
    [SerializeField] List<Sprite> itemImage = new List<Sprite>();
    [SerializeField] List<Image> armorsImage = new List<Image>();
    [SerializeField] List<Image> swordsImage = new List<Image>();
    [SerializeField] List<GameObject> characterArmors = new List<GameObject>();
    [SerializeField] List<GameObject> characterSwords = new List<GameObject>();

    public void CharacterStatStart()
    {
        ArmorRestart();
        ArmorOpen();
        SwordsRestart();
        SwordsOpen();
    }

    public void SetVillagePanel(int villageCount, bool tempBool)
    {
        villagePanel[villageCount].SetActive(tempBool);
    }

    public Image GetArmorsImage(int villageCount)
    {
        return armorsImage[villageCount];
    }
    public Image GetSwordsImage(int villageCount)
    {
        return swordsImage[villageCount];
    }
    public Sprite GetItemImage(int villageCount)
    {
        return itemImage[villageCount];
    }

    public void ArmorUpgrade()
    {
        ArmorRestart();
        ArmorOpen();
    }
    public void SwordUpgrade()
    {
        SwordsRestart();
    }


    private void ArmorRestart()
    {
        foreach (GameObject item in characterArmors) item.SetActive(false);
    }
    private void ArmorOpen()
    {
        characterArmors[ItemData.Instance.factor.characterHealth].SetActive(true);
    }
    private void SwordsRestart()
    {
        foreach (GameObject item in characterSwords) item.SetActive(false);
    }
    private void SwordsOpen()
    {
        characterSwords[(int)ItemData.Instance.factor.characterHitTime].SetActive(true);
    }

}
