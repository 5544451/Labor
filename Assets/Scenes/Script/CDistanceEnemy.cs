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

    //Ÿ���ϴ� ������Ʈ
    //private GameObject AtkObject;
    private bool chase = false;

    //���糲�� ���� ��Ÿ��
    private float curTime = 0.7f;
    //���ݱ��� ���� ��Ÿ�� 
    private const float coolTime = 1.5f;

    // ���� �ٶ󺸴� ����
    private Vector2 enemyForward, direction, currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);

        //if (targetTransform == null)
        //{
        //    Debug.LogError("TargetTransform�� �Ҵ�ȵ�");
        //}

        rb = GetComponent<Rigidbody2D>();
        //if (rb == null)
        //{
        //    Debug.LogError("Rigidbody2D ������Ʈ�� ����.");
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
            ChaseAndAttack(); // ���� + ���ݽ���
        }

    }

    private void ChaseAndAttack()
    {
        distance = Vector2.Distance(transform.position, targetTransform.position);

        if (distance <= 0.6f) //���� ���� �ȿ� ���Ӵ��� �Ǻ�
        {
            if(curTime <= 0) // ���� ��Ÿ�� �ٵǸ� �غ� �� ����
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

        // ���� �Ÿ� �̻� �������� ���� ����
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
                Debug.Log("�÷��̾� ����");
            }
        }
    }

    public void Move(Transform targetPosition, float speed) //rigidbody ���� �Լ� & ĳ�� �¿����
    {
        Vector2 direction = (targetPosition.position - transform.position).normalized;
        currentVelocity = direction * speed;
        rb.velocity = currentVelocity;

        // �¿� ����
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

    public void ApplyKnockback(Vector2 knockbackForce) // �˹�
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
        while (true) // ���� ���� �߰�
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
            return false; // ���� �Ÿ� �̻��̸� �ٷ� ��ȯ

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
