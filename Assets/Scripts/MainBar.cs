using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBar : MonoBehaviour
{
    [SerializeField] Image barImage;

    void Update()
    {
        UpdateHealthBar();
        barImage.transform.parent.transform.LookAt(EnemyBarManager.Instance.GetCamera().transform);
    }

    void UpdateHealthBar()
    {
        float targetFillAmount = (float)CharacterManager.Instance.GetCharacterHealth() / (float)ItemData.Instance.field.characterHealth;
        barImage.fillAmount = Mathf.Lerp(barImage.fillAmount, targetFillAmount, Time.deltaTime * EnemyBarManager.Instance.GetBarFloat());
    }
}
