using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItemSystem : MonoSingleton<ThrowItemSystem>
{
    [SerializeField] int itemMainOPCount;
    [SerializeField] int itemThrowTime;
    [SerializeField] int itemColligateTime;
    [SerializeField] float itemThrowPower;
    [SerializeField] GameObject finishPositionItem;
    [SerializeField] float finishMoveSpeed;


    public IEnumerator LaunchRandomItems(int itemCount, int tagCount, GameObject deathThrowCoinOP)
    {
        bool tempIsMove = true;
        List<GameObject> tempItems = new List<GameObject>();

        for (int i = 0; i < itemCount; i++)
        {
            GameObject obj = ObjectPool.Instance.GetPooledObject(tagCount + itemMainOPCount, deathThrowCoinOP.transform.position);
            obj.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(0, itemThrowPower), 7, Random.Range(0, itemThrowPower));
            tempItems.Add(obj);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(itemThrowTime);

        for (int i = 0; i < itemCount; i++)
        {
            GameObject obj = tempItems[i];
            MoveMechanics.Instance.MoveLerp(obj, finishPositionItem, finishMoveSpeed, ref tempIsMove, () => { ObjectPool.Instance.AddObject(tagCount + itemMainOPCount, obj); });
            ItemManager.Instance.UPItemUICount(tagCount, 1);
        }

        ItemManager.Instance.ReWriteItemCount();
    }
}
