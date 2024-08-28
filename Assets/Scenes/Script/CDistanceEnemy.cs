using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_moving : MonoBehaviour
{
    GameObject enemyRecog;
    private float characterScale, distance;
    private NavMeshAgent agent;

    [SerializeField] Transform[] m_WayPoints = null;
    [SerializeField] Transform targetTransform = null;
    int m_WayPointsCount = 0;

    bool chase = false;

    void MoveWayPoint()
    {
 
        if (agent.remainingDistance < 1.3f)
        {
            agent.SetDestination(m_WayPoints[m_WayPointsCount++].position);

            if (m_WayPointsCount >= m_WayPoints.Length)
                m_WayPointsCount = 0;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        InvokeRepeating("MoveWayPoint", 0f, 3f);

    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, targetTransform.position);

        if(distance < 3.0f)
        {
            if (!chase)
            {
                enemyRecog.SetActive(true);
                agent.speed = 3f;
                CancelInvoke("MoveWayPoint");
            }
            agent.SetDestination(targetTransform.transform.position);
            chase = true;
        }
        else
        {
            if (chase)
            {
                agent.speed = 1f;
                enemyRecog.SetActive(false);
                InvokeRepeating("MoveWayPoint", 3f, 3f);
            }
            chase = false;
        }

    }
}
