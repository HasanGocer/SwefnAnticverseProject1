using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
        public List<float> HitTime = new List<float>();
        public List<int> itemHitCount = new List<int>();
        public int characterHealth;
        public int characterAttack;
        public float characterHitTime;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void AwakeID()
    {
        for (int i = 0; i < field.HitTime.Count; i++)
        {
            field.HitTime[i] = standart.HitTime[i] - (factor.HitTime[i] * constant.HitTime[i]);
            fieldPrice.HitTime[i] = fieldPrice.HitTime[i] * factor.HitTime[i];
        }

        for (int i = 0; i < field.itemHitCount.Count; i++)
        {
            field.itemHitCount[i] = standart.itemHitCount[i] + (factor.itemHitCount[i] * constant.itemHitCount[i]);
            fieldPrice.itemHitCount[i] = fieldPrice.itemHitCount[i] * factor.itemHitCount[i];
        }

        field.characterHealth = standart.characterHealth + (factor.characterHealth * constant.characterHealth);
        fieldPrice.characterHealth = fieldPrice.characterHealth * factor.characterHealth;

        field.characterAttack = standart.characterAttack + (factor.characterAttack * constant.characterAttack);
        fieldPrice.characterAttack = fieldPrice.characterAttack * factor.characterAttack;

        field.characterHitTime = standart.characterHitTime + (factor.characterHitTime * constant.characterHitTime);
        fieldPrice.characterHitTime = fieldPrice.characterHitTime * factor.characterHitTime;
        /*
         field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        */

        for (int i = 0; i < field.HitTime.Count; i++)
        {
            if (factor.HitTime[i] > maxFactor.HitTime[i])
            {
                factor.HitTime[i] = maxFactor.HitTime[i];
                field.HitTime[i] = standart.HitTime[i] - (factor.HitTime[i] * constant.HitTime[i]);
                fieldPrice.HitTime[i] = fieldPrice.HitTime[i] / (factor.HitTime[i] - 1);
                fieldPrice.HitTime[i] = fieldPrice.HitTime[i] * factor.HitTime[i];
            }
        }

        for (int i = 0; i < field.itemHitCount.Count; i++)
        {
            if (factor.itemHitCount[i] > maxFactor.itemHitCount[i])
            {
                factor.itemHitCount[i] = maxFactor.itemHitCount[i];
                field.itemHitCount[i] = standart.itemHitCount[i] + (factor.itemHitCount[i] * constant.itemHitCount[i]);
                fieldPrice.itemHitCount[i] = fieldPrice.itemHitCount[i] / (factor.itemHitCount[i] - 1);
                fieldPrice.itemHitCount[i] = fieldPrice.itemHitCount[i] * factor.itemHitCount[i];
            }
        }

        if (factor.characterHealth > maxFactor.characterHealth)
        {
            factor.characterHealth = maxFactor.characterHealth;
            field.characterHealth = standart.characterHealth + (factor.characterHealth * constant.characterHealth);
            fieldPrice.characterHealth = fieldPrice.characterHealth / (factor.characterHealth - 1);
            fieldPrice.characterHealth = fieldPrice.characterHealth * factor.characterHealth;
        }

        if (factor.characterAttack > maxFactor.characterAttack)
        {
            factor.characterAttack = maxFactor.characterAttack;
            field.characterAttack = standart.characterAttack + (factor.characterAttack * constant.characterAttack);
            fieldPrice.characterAttack = fieldPrice.characterAttack / (factor.characterAttack - 1);
            fieldPrice.characterAttack = fieldPrice.characterAttack * factor.characterAttack;
        }

        if (factor.characterHitTime > maxFactor.characterHitTime)
        {
            factor.characterHitTime = maxFactor.characterHitTime;
            field.characterHitTime = standart.characterHitTime + (factor.characterHitTime * constant.characterHitTime);
            fieldPrice.characterHitTime = fieldPrice.characterHitTime / (factor.characterHitTime - 1);
            fieldPrice.characterHitTime = fieldPrice.characterHitTime * factor.characterHitTime;
        }

        /*
          if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }
        */

        StartCoroutine(Buttons.Instance.LoadingScreen());
    }

    public void SetHitTime(int tagCount)
    {
        factor.HitTime[tagCount]++;

        field.HitTime[tagCount] = standart.HitTime[tagCount] - (factor.HitTime[tagCount] * constant.HitTime[tagCount]);
        fieldPrice.HitTime[tagCount] = fieldPrice.HitTime[tagCount] / (factor.HitTime[tagCount] - 1);
        fieldPrice.HitTime[tagCount] = fieldPrice.HitTime[tagCount] * factor.HitTime[tagCount];

        if (factor.HitTime[tagCount] > maxFactor.HitTime[tagCount])
        {
            factor.HitTime[tagCount] = maxFactor.HitTime[tagCount];
            field.HitTime[tagCount] = standart.HitTime[tagCount] - (factor.HitTime[tagCount] * constant.HitTime[tagCount]);
            fieldPrice.HitTime[tagCount] = fieldPrice.HitTime[tagCount] / (factor.HitTime[tagCount] - 1);
            fieldPrice.HitTime[tagCount] = fieldPrice.HitTime[tagCount] * factor.HitTime[tagCount];
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }


    public void SetHitCount(int tagCount)
    {
        factor.itemHitCount[tagCount]++;

        field.itemHitCount[tagCount] = standart.itemHitCount[tagCount] + (factor.itemHitCount[tagCount] * constant.itemHitCount[tagCount]);
        fieldPrice.itemHitCount[tagCount] = fieldPrice.itemHitCount[tagCount] / (factor.itemHitCount[tagCount] - 1);
        fieldPrice.itemHitCount[tagCount] = fieldPrice.itemHitCount[tagCount] * factor.itemHitCount[tagCount];

        if (factor.itemHitCount[tagCount] > maxFactor.itemHitCount[tagCount])
        {
            factor.itemHitCount[tagCount] = maxFactor.itemHitCount[tagCount];
            field.itemHitCount[tagCount] = standart.itemHitCount[tagCount] + (factor.itemHitCount[tagCount] * constant.itemHitCount[tagCount]);
            fieldPrice.itemHitCount[tagCount] = fieldPrice.itemHitCount[tagCount] / (factor.itemHitCount[tagCount] - 1);
            fieldPrice.itemHitCount[tagCount] = fieldPrice.itemHitCount[tagCount] * factor.itemHitCount[tagCount];
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetCharacterHealth()
    {
        factor.characterHealth++;

        field.characterHealth = standart.characterHealth + (factor.characterHealth * constant.characterHealth);
        fieldPrice.characterHealth = fieldPrice.characterHealth / (factor.characterHealth - 1);
        fieldPrice.characterHealth = fieldPrice.characterHealth * factor.characterHealth;

        if (factor.characterHealth > maxFactor.characterHealth)
        {
            factor.characterHealth = maxFactor.characterHealth;
            field.characterHealth = standart.characterHealth + (factor.characterHealth * constant.characterHealth);
            fieldPrice.characterHealth = fieldPrice.characterHealth / (factor.characterHealth - 1);
            fieldPrice.characterHealth = fieldPrice.characterHealth * factor.characterHealth;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetCharacterAttack()
    {
        factor.characterAttack++;

        field.characterAttack = standart.characterAttack + (factor.characterAttack * constant.characterAttack);
        fieldPrice.characterAttack = fieldPrice.characterAttack / (factor.characterAttack - 1);
        fieldPrice.characterAttack = fieldPrice.characterAttack * factor.characterAttack;

        if (factor.characterAttack > maxFactor.characterAttack)
        {
            factor.characterAttack = maxFactor.characterAttack;
            field.characterAttack = standart.characterAttack + (factor.characterAttack * constant.characterAttack);
            fieldPrice.characterAttack = fieldPrice.characterAttack / (factor.characterAttack - 1);
            fieldPrice.characterAttack = fieldPrice.characterAttack * factor.characterAttack;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    public void SetCharacterHitTime()
    {
        factor.characterHitTime++;

        field.characterHitTime = standart.characterHitTime + (factor.characterHitTime * constant.characterHitTime);
        fieldPrice.characterHitTime = fieldPrice.characterHitTime / (factor.characterHitTime - 1);
        fieldPrice.characterHitTime = fieldPrice.characterHitTime * factor.characterHitTime;

        if (factor.characterHitTime > maxFactor.characterHitTime)
        {
            factor.characterHitTime = maxFactor.characterHitTime;
            field.characterHitTime = standart.characterHitTime + (factor.characterHitTime * constant.characterHitTime);
            fieldPrice.characterHitTime = fieldPrice.characterHitTime / (factor.characterHitTime - 1);
            fieldPrice.characterHitTime = fieldPrice.characterHitTime * factor.characterHitTime;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }

    /*
     public void SetObjectCount()
    {
        factor.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    */
}
