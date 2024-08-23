using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_charactor : MonoBehaviour
{
    Rigidbody2D rig;
    private Animator animator;

    private float characterScale;
    private float moveSpeed = 0.5f; //�⺻�̼�
    private float maxSpeed = 8; // �ִ��̼�
    private float jumpPower = 35; // ����
    private bool isJumping, jump;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isJumping = false; //���� �����ϴ� ���ΰ�? 1�������� �ϵ��� ����
        jump = true; //��������? Ű �ѹ��� ���� �ѹ��� 
    }
    void OnCollisionEnter2D(Collision2D collision) //�浹 ����
    {
        if (collision.gameObject.tag == "Ground")
        { //tag�� Floor�� ������Ʈ�� �浹���� ��
            isJumping = false; //isJumping�� �ٽ� false��
        }

    }
/*    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Console.WriteLine("TriggerEnter" + other.gameObject.name);
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Console.WriteLine("TriggerEnter" + other.gameObject.name);
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))//move_left
        {
            rig.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);  // ��ü�� ���� �������� ������ ���� ������
            rig.velocity = new Vector2(Mathf.Max(rig.velocity.x, -maxSpeed), rig.velocity.y); //���� �ӵ��� �����ϸ� �� �̻� �������� �ʰ���.
            //rig.velocity = new Vector2(-moveSpeed, rig.velocity.y);
            transform.localScale = new Vector3(-characterScale, characterScale, 1);

            //animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rig.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);// ��ü�� ������ �������� ������ ���� ������.
            rig.velocity = new Vector2(Mathf.Min(rig.velocity.x, maxSpeed), rig.velocity.y); //���� �ӵ��� �����ϸ� �� �̻� �������� �ʰ���.
            //rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
            transform.localScale = new Vector3(characterScale, characterScale, 1);
            //animator.SetBool("moving", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) // �̵� Ű�� �� ���
        {
            // x �ӵ��� �ٿ� �̵� �������� ���� ��¦�� �����̰� ���� �ٷ� ���߰� �մϴ�.
            rig.velocity = new Vector2(rig.velocity.normalized.x, rig.velocity.y);
            //animator.SetBool("moving", false);
        }

        // �� ȭ��ǥ�� ������ �����մϴ�.
        if (Input.GetKey(KeyCode.UpArrow) && !isJumping && jump)
        {
            rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
            jump = false;
            //Debug.Log("jump");
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) //��Ű �ѹ��� ���� �ѹ���
        {
            jump = true;
        }

    }

}
