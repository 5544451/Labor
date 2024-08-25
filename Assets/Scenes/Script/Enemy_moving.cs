using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_moving : MonoBehaviour
{
    GameObject enemyRecog;
    private float characterScale;
    /*    private float moveSpeed = 0.3f; //기본이속
        private float maxSpeed = 7; // 최대이속
        private float jumpPower = 20; // 점속*/

    GameObject targetTransform = null;
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);

        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            enemyRecog.SetActive(true);
        }
        //플레이어가 어디있는지 (좌,우  확인)
        //플레이어 방향으로 이동 + 플레이어와의 거리 계산 반복
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //enemyRecog.SetActive(true);
            targetTransform = other.gameObject;
            agent.SetDestination(targetTransform.transform.position);
            //Debug.Log(agent.remainingDistance);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemyRecog.SetActive(false);
        }
    }


}
