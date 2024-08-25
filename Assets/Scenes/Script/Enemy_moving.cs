using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_moving : MonoBehaviour
{
    GameObject enemyRecog;
    private float characterScale;
    /*    private float moveSpeed = 0.3f; //�⺻�̼�
        private float maxSpeed = 7; // �ִ��̼�
        private float jumpPower = 20; // ����*/

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
        //�÷��̾ ����ִ��� (��,��  Ȯ��)
        //�÷��̾� �������� �̵� + �÷��̾���� �Ÿ� ��� �ݺ�
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
