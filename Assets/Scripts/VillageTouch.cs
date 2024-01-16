using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageTouch : MonoBehaviour
{
    [SerializeField] int villagerCount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Main"))
            VillageManager.Instance.SetVillagePanel(villagerCount, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Main"))
            VillageManager.Instance.SetVillagePanel(villagerCount, false);
    }
}
