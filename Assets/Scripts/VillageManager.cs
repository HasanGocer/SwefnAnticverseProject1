using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageManager : MonoSingleton<VillageManager>
{
    [SerializeField] List<GameObject> villagePanel = new List<GameObject>();

    public void SetVillagePanel(int villageCount, bool tempBool)
    {
        villagePanel[villageCount].SetActive(tempBool);
    }

}
