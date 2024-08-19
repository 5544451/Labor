using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_charactor : MonoBehaviour
{
    Rigidbody2D rig;
    private Animator animator;

    private float characterScale;
    private float moveSpeed = 0.3f;
    private float maxSpeed = 10;
    private float jumpPower = 30;
    private bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isJumping = false;
    }
    void OnCollisionEnter2D(Collision2D collision) //�浹 ����
    {
        //Debug.Log("collision : " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Ground")
        { //tag�� Floor�� ������Ʈ�� �浹���� ��
            isJumping = false; //isJumping�� �ٽ� false��
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))//move_left
        {
            rig.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);  // ��ü�� ���� �������� ������ ���� ������
            rig.velocity = new Vector2(Mathf.Max(rig.velocity.x, -maxSpeed), rig.velocity.y); //���� �ӵ��� �����ϸ� �� �̻� �������� �ʰ���.
            transform.localScale = new Vector3(-characterScale, characterScale, 1);

            animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rig.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);// ��ü�� ������ �������� ������ ���� ������.
            rig.velocity = new Vector2(Mathf.Min(rig.velocity.x, maxSpeed), rig.velocity.y); //���� �ӵ��� �����ϸ� �� �̻� �������� �ʰ���.
            transform.localScale = new Vector3(characterScale, characterScale, 1);

            animator.SetBool("moving", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) // �̵� Ű�� �� ���
        {
            // x �ӵ��� �ٿ� �̵� �������� ���� ��¦�� �����̰� ���� �ٷ� ���߰� �մϴ�.
            rig.velocity = new Vector3(rig.velocity.normalized.x, rig.velocity.y);
            animator.SetBool("moving", false);
        }

        // �� ȭ��ǥ�� ������ �����մϴ�.
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJumping)
        {
            rig.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
            //Debug.Log("jump");
        }

        //var vel_norm = rig.velocity.normalized;

    }



}
