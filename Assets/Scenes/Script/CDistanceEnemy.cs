using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CDistanceEnemy : MonoBehaviour
{
    GameObject enemyRecog;
    private float characterScale, distance;

    [SerializeField] Transform[] m_WayPoints = null;
    [SerializeField] Transform targetTransform = null;
    int m_WayPointsCount = 0;
    private float speed = 1.5f;

    bool chase = false;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);

        StartCoroutine(MoveWayPoint());
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, targetTransform.position);

        if(distance < 2.5f)
        {
            if (!chase)
            {
                enemyRecog.SetActive(true);
                StopAllCoroutines();
            }
            transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * 2.3f);
            chase = true;
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
}
