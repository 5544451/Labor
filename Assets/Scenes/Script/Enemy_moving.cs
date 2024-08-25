using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_moving : MonoBehaviour
{
    GameObject enemyRecog;
    private float characterScale, distance;
    /*    private float moveSpeed = 0.3f; //기본이속
        private float maxSpeed = 7; // 최대이속
        private float jumpPower = 20; // 점속*/

    //GameObject targetTransform = null;
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




/*    void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.gameObject.name);
        if (other.CompareTag("Player"))
        {




            //enemyRecog.SetActive(true);
            targetTransform = other.gameObject;

            agent.SetDestination(targetTransform.transform.position);
            //Debug.Log("remainingDistance : " +agent.remainingDistance);

            if (agent.remainingDistance > 4.0f)
            {

            }



        }
        //플레이어 방향으로 이동 + 플레이어와의 거리 계산 반복
    }*/

/*    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            agent.speed = 1f;
            enemyRecog.SetActive(false);
            InvokeRepeating("MoveWayPoint", 0f, 5f);
        }
    }*/
}
