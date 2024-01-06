using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarManager : MonoSingleton<EnemyBarManager>
{
    [SerializeField] float barSpeed;

    public float GetBarFloat()
    {
        return barSpeed;
    }
}
