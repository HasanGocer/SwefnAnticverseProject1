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
    [SerializeField] Rigidbody rb;
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
            transform.LookAt(CharacterManager.Instance.GetCharacter().transform.position);
            Vector3 way = Vector3.Normalize(transform.position - CharacterManager.Instance.GetCharacter().transform.position) * EnemyFightManager.Instance.GetWalkSpeed();
            way = new Vector3(way.x, 0, way.z);
            rb.velocity = way;
            yield return null;
        }
        yield return null;
    }
}
