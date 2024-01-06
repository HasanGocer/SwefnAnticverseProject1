using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarManager : MonoSingleton<EnemyBarManager>
{
    [SerializeField] float barSpeed;
    [SerializeField] GameObject _camera;

    public GameObject GetCamera()
    {
        return _camera;
    }
    public float GetBarFloat()
    {
        return barSpeed;
    }
}
