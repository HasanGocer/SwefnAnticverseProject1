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
    Transform target;
    private void OnEnable()
    {
        isHit = false;
        isWalk = false;
    }
    private void Start()
    {
        target = CharacterManager.Instance.GetCharacter().transform;

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
            else if (Vector3.Distance(CharacterManager.Instance.GetCharacter().transform.position, transform.position) < EnemyFightManager.Instance.GetMinViewDistance() && Vector3.Distance(CharacterManager.Instance.GetCharacter().transform.position, transform.position) > EnemyFightManager.Instance.GetMinHitDistance() && !isHit)
            {
                isWalk = true;
                enemyAnim.CallRunAnim();
                StartCoroutine(WalkToCharacter());
            }
            else if (!isHit)
            {
                isWalk = false;
                enemyAnim.CallIdleAnim();
            }


        if (GameManager.Instance.gameStat == GameManager.GameStat.start && isWalk && enemyManager.GetIsLive())
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            transform.LookAt(target.position);
            rb.velocity = directionToTarget * EnemyFightManager.Instance.GetWalkSpeed();
        }
        else
            rb.velocity = Vector3.zero;
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
    private IEnumerator WalkToCharacter()
    {
        isWalk = true;
        while (Vector3.Distance(CharacterManager.Instance.GetCharacter().transform.position, transform.position) > EnemyFightManager.Instance.GetMinHitDistance() && Vector3.Distance(CharacterManager.Instance.GetCharacter().transform.position, transform.position) < EnemyFightManager.Instance.GetMinViewDistance() && enemyManager.GetIsLive())
        {
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            transform.LookAt(target.position);
            rb.velocity = directionToTarget * EnemyFightManager.Instance.GetWalkSpeed();

            yield return new WaitForSeconds(Time.deltaTime);
        }

        isWalk = false;
        rb.velocity = Vector3.zero;
    }
}
