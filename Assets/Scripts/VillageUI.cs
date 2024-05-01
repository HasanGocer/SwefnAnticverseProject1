using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VillageUI : MonoBehaviour
{
    [SerializeField] Button _backButton;
    [SerializeField] int _villageCount;
    [SerializeField] bool isBlackSmith;
    [SerializeField] List<int> _villageItemTag = new List<int>();
    [SerializeField] List<Text> _villageItemPrice = new List<Text>();
    [SerializeField] List<Image> _villageItemImage = new List<Image>();

    public void PanelOpen()
    {
        if (isBlackSmith) if (ItemData.Instance.factor.characterAttack < 4)
                for (int i = 0; i < ItemData.Instance.factor.characterAttack; i++)
                    AttackFunc(i);
            else for (int i = 0; i < 3; i++)
                    AttackFunc(i);
        else if (ItemData.Instance.factor.characterAttack < 4)
            for (int i = 0; i < ItemData.Instance.factor.characterHealth; i++) HealthFunc(i);
        else for (int i = 0; i < 3; i++)
                AttackFunc(i);

    }

    private void Start()
    {
        _backButton.onClick.AddListener(BackButtonFunc);
        foreach (Image item in _villageItemImage) item.gameObject.SetActive(false);
    }

    private void AttackFunc(int i)
    {
        _villageItemImage[i].gameObject.SetActive(true);
        _villageItemImage[i].sprite = VillageManager.Instance.GetItemImage(i);
        _villageItemTag[i] = i;
        _villageItemPrice[i].text = ItemData.Instance.fieldPrice.characterAttack.ToString();
    }
    private void HealthFunc(int i)
    {
        _villageItemImage[i].gameObject.SetActive(true);
        _villageItemImage[i].sprite = VillageManager.Instance.GetItemImage(i);
        _villageItemTag[i] = i;
        _villageItemPrice[i].text = ItemData.Instance.fieldPrice.characterHealth.ToString();

    }

    private void BackButtonFunc()
    {
        VillageManager.Instance.SetVillagePanel(_villageCount, false);
    }
}
