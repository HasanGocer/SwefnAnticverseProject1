using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static ToonyColorsPro.ShaderGenerator.Enums;

public class CharacterFight : MonoBehaviour
{
    [SerializeField] CharacterManager characterManager;
    [SerializeField] GameObject _target = null;
    bool isHit = false, isInner = false;
    float _hitTimer = 0;
    EnemyManager enemyManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && _target == null)
        {
            _target = other.gameObject;
            enemyManager = other.GetComponent<EnemyManager>();
            if (enemyManager.GetEnemyHealth() > 0) isInner = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy")) if (_target == other.gameObject) { isInner = false; }
    }

    private void Update()
    {
        if (!isHit && isInner && _target != null)
            if (enemyManager.GetEnemyHealth() > 0)
            {
                isHit = true;
                characterManager.GetAnimController().CallHitAnim();
                _hitTimer = ItemData.Instance.field.characterHitTime;
            }
        if (_target != null)
            if (enemyManager.GetEnemyHealth() <= 0)
            {
                characterManager.GetAnimController().SetHitBool(false);
                JoystickInput.Instance.SetIsIdle(false);
                _target = null;
                isInner = false;
                enemyManager = null;
            }
        if (!isInner)
        {
            characterManager.GetAnimController().SetHitBool(false);
            JoystickInput.Instance.SetIsIdle(false);
            _target = null;
            enemyManager = null;
        }
        if (isHit)
        {
            _hitTimer -= Time.deltaTime;
            if (_hitTimer <= 0f && enemyManager != null)
            {
                enemyManager.DownEnemyHeallth(ItemData.Instance.field.characterAttack);
                isHit = false;
            }
            else if (_hitTimer <= 0f)
                isHit = false;
        }
    }

    public bool GetIsHit() { return isHit; }

}
