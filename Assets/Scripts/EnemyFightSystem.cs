using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class EnemyFightSystem : MonoBehaviour
{
    private bool isWalk, isHit;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] Collider hitCollider;
    [SerializeField] EnemyAnim enemyAnim;
    [SerializeField] EnemyCollider enemyCollider;
    private void OnEnable()
    {
        isHit = false;
        isWalk = false;
    }

    private void Update()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && enemyManager.GetIsLive())
            if (Vector3.Distance(CharacterManager.Instance.GetCharacter().transform.position, transform.position) < EnemyFightManager.Instance.GetMinHitDistance() && !isHit)
            {
                isWalk = false;
                isHit = true;
                StartCoroutine(AttackCharacter());
            }
            else if (Vector3.Distance(CharacterManager.Instance.GetCharacter().transform.position, transform.position) < EnemyFightManager.Instance.GetMinViewDistance() && Vector3.Distance(CharacterManager.Instance.GetCharacter().transform.position, transform.position) > EnemyFightManager.Instance.GetMinHitDistance() && !isWalk && !isHit)
            {
                isWalk = true;
                enemyAnim.CallRunAnim();
                StartCoroutine(StartWalking());
            }
            else if (!isHit)
            {
                isWalk = false;
                enemyAnim.CallIdleAnim();
            }
    }

    public EnemyAnim GetEnemyAnim() { return enemyAnim; }

    IEnumerator AttackCharacter()
    {
        if (enemyManager.GetIsLive())
            enemyAnim.CallHitAnim();
        yield return new WaitForSeconds(1f);
        if (enemyManager.GetIsLive())
            hitCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(EnemyFightManager.Instance.GetEnemyAttackCountDown(enemyManager.GetEnemyCount()) - 1f);
        if (enemyManager.GetIsLive())
            hitCollider.gameObject.SetActive(false);
        if (enemyManager.GetIsLive())
            isHit = false;
    }
    IEnumerator StartWalking()
    {
        while (isWalk && enemyManager.GetIsLive())
        {
            Vector3 direction = (CharacterManager.Instance.GetCharacter().transform.position - transform.position);
            Vector3 normalizedDirection = direction.normalized;
            Vector3 movement = normalizedDirection * EnemyFightManager.Instance.GetWalkSpeed() * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.Translate(movement, Space.World);
            yield return null;
        }
        yield return null;
    }
}
