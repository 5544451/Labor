using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class CDistanceEnemy : MonoBehaviour
{
    private GameObject enemyRecog;
    private float characterScale, distance, thresholdDistance, chaseDirection;
    private Rigidbody2D rb;

    [SerializeField] Transform[] m_WayPoints;
    [SerializeField] Transform targetTransform;
    [SerializeField] Transform hitpos;
    [SerializeField] Vector2 boxSize;
    private int m_WayPointsCount = 0;
    private const float speed = 2.3f;

    //타격하는 오브젝트
    //private GameObject AtkObject;
    private bool chase = false;

    //현재남은 공격 쿨타임
    private float curTime = 0.7f;
    //공격까지 남은 쿨타임 
    private const float coolTime = 1.5f;

    // 적이 바라보는 방향
    private Vector2 enemyForward, direction, currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);

        //if (targetTransform == null)
        //{
        //    Debug.LogError("TargetTransform이 할당안됨");
        //}

        rb = GetComponent<Rigidbody2D>();
        //if (rb == null)
        //{
        //    Debug.LogError("Rigidbody2D 컴포넌트가 없음.");
        //}

        StartCoroutine(MoveWayPoint());
        thresholdDistance = 3.0f;
    }

    private void Update()
    {
        if (IsTargetInSight() && !chase)
        {
            enemyRecog.SetActive(true);
            StopAllCoroutines();
            chase = true;
        }

        if(chase)
        {
            ChaseAndAttack(); // 추적 + 공격시작
        }

    }

    private void ChaseAndAttack()
    {
        distance = Vector2.Distance(transform.position, targetTransform.position);

        if (distance <= 0.6f) //공격 범위 안에 들어왓는지 판별
        {
            if(curTime <= 0) // 공격 쿨타임 다되면 준비 후 공격
            {
                PerformAttack();
                curTime = coolTime;
            }
            else
            {
                curTime -= Time.deltaTime;
            }
        }
        else
        {
            Move(targetTransform, speed);
        }

        // 일정 거리 이상 떨어지면 추적 중지
        if (distance > 4.0f)
        {
            StopChase();
        }
    }

    private void PerformAttack()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(hitpos.position, boxSize, 0);
        foreach (Collider2D collider2D in collider2Ds)
        {
            if (collider2D.CompareTag("Player"))
            {
                //AtkObject = collider2D.gameObject;
                //AtkObject.GetComponent<HPController>().GetDamage(50, transform.position);
                Debug.Log("플레이어 공격");
            }
        }
    }

    public void Move(Transform targetPosition, float speed) //rigidbody 조정 함수 & 캐릭 좌우반전
    {
        Vector2 direction = (targetPosition.position - transform.position).normalized;
        currentVelocity = direction * speed;
        rb.velocity = currentVelocity;

        // 좌우 반전
        chaseDirection = targetPosition.position.x - transform.position.x;
        if (chaseDirection < 0)
        {
            transform.localScale = new Vector3(-characterScale, characterScale, 1);
            enemyForward = transform.right * -1;
        }
        else
        {
            transform.localScale = new Vector3(characterScale, characterScale, 1);
            enemyForward = transform.right;
        }

    }

    public void ApplyKnockback(Vector2 knockbackForce) // 넉백
    {
        float force = 5.0f;
        rb.AddForce(knockbackForce*force, ForceMode2D.Impulse);
    }

    private void StopChase()
    {
        chase = false;
        enemyRecog.SetActive(false);
        StartCoroutine(MoveWayPoint());
    }

    IEnumerator MoveWayPoint()
    {
        while (true) // 무한 루프 추가
        {
            Move(m_WayPoints[m_WayPointsCount], speed/2);

            if (Vector2.Distance(transform.position, m_WayPoints[m_WayPointsCount].position) <= 0.5f)
            {
                m_WayPointsCount++;
                yield return new WaitForSeconds(2);
                if (m_WayPointsCount == m_WayPoints.Length)
                {
                    m_WayPointsCount = 0;
                }
            }
            yield return null;
        }
    }

    bool IsTargetInSight()
    {
        float distanceToTarget = Vector2.Distance(transform.position, targetTransform.position);

        if (distanceToTarget > thresholdDistance)
            return false; // 일정 거리 이상이면 바로 반환

        Vector2 directionToTarget = (targetTransform.position - transform.position).normalized;
        float angleToTarget = Vector2.Angle(enemyForward, directionToTarget);

        return angleToTarget <= 90 / 2;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(hitpos.position, boxSize);
    }
}
