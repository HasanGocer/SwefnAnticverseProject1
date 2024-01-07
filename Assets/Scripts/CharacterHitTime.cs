using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHitTime : MonoBehaviour
{
    [SerializeField] CharacterFight characterFight;
    List<GameObject> enemys = new List<GameObject>();

    private void OnEnable()
    {
        enemys.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
            if (IsHere(other.gameObject))
            {
                EnemyManager enemyManager = other.GetComponent<EnemyManager>();
                enemyManager.DownEnemyHeallth(ItemData.Instance.field.characterAttack);
            }
    }

    private bool IsHere(GameObject tempObject)
    {
        bool tempBool = true;

        for (int i = 0; i < enemys.Count; i++)
            if (enemys[i] == tempObject) tempBool = false;
        if (tempBool) enemys.Add(tempObject);
        return tempBool;
    }
}
