using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_moving : MonoBehaviour
{
    Rigidbody2D rig;
    GameObject enemyRecog;
    private float characterScale;
    private float moveSpeed = 0.3f; //�⺻�̼�
    private float maxSpeed = 7; // �ִ��̼�
    private float jumpPower = 20; // ����

    Transform tragetTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);
        rig = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //print(other.gameObject.name);
        if (other.gameObject.CompareTag("Player"))
        {
            enemyRecog.SetActive(true);
            tragetTransform = other.gameObject.transform;
        }
        //�÷��̾ ����ִ��� (��,��  Ȯ��)
        //�÷��̾� �������� �̵� + �÷��̾���� �Ÿ� ��� �ݺ�


    }

/*    IEnumerator Move(Rigidbody2D rig, float moveSpeed)
    {
        float remainDistance = (transform.position - tragetTransform.position).sqrMagnitude;


    }*/
}
