using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CDistanceEnemy : MonoBehaviour
{
    private GameObject enemyRecog;
    private float characterScale, distance, thresholdDistance, chaseDirection;

    [SerializeField] Transform[] m_WayPoints = null;
    [SerializeField] Transform targetTransform = null;
    [SerializeField] Transform hitpos;
    [SerializeField] Vector2 boxSize;
    private int m_WayPointsCount = 0;
    private float speed = 1.5f;

    private GameObject AtkObject;
    private bool chase = false;

    private float curTime;
    private float coolTime = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);

        StartCoroutine(MoveWayPoint());
        thresholdDistance = 2.5f * 2.5f;
    }

    private void Update()
    {
        distance = (transform.position - targetTransform.position).sqrMagnitude;

        if (distance < thresholdDistance)
        {
            if (!chase)
            {
                enemyRecog.SetActive(true);
                StopAllCoroutines();
            }
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * 2.3f);

            chaseDirection = targetTransform.position.x - transform.position.x;
            if (chaseDirection < 0)
            {
                transform.localScale = new Vector3(-characterScale, characterScale, 1);
            }
            else
            {
                transform.localScale = new Vector3(characterScale, characterScale, 1);
            }

            chase = true;

            if(distance < thresholdDistance)
            {
                if (curTime <= 0) //공격 쿨타임 조절
                {
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(hitpos.position, boxSize, 0); //공격범위안에 뭐들어왔나 확인
                        foreach (Collider2D collider2D in collider2Ds) //여러개면 다출력
                        {
                            if (collider2D.tag == "Player")//범위안에 들어간거 hp있으면 hp빼는 작업
                            {
                                AtkObject = collider2D.gameObject;
                                AtkObject.GetComponent<HPController>().GetDamage(20);
                            }
                            Debug.Log(collider2D.tag);
                        }
                        curTime = coolTime;
                    }
                }
                else
                {
                    curTime -= Time.deltaTime;
                }
            }

        }
        else
        {
            if (chase)
            {
                enemyRecog.SetActive(false);
                StartCoroutine(MoveWayPoint());
            }
            chase = false;
        }

    }
    IEnumerator MoveWayPoint()
    {
        while (true) // 무한 루프 추가
        {
            transform.position = Vector2.MoveTowards(transform.position, m_WayPoints[m_WayPointsCount].position, Time.deltaTime * speed);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(hitpos.position, boxSize);
    }
}
