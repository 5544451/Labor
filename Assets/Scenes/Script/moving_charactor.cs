using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_charactor : MonoBehaviour
{

    const string oneWayPlatformLayerName = "OneWayPlatform";
    const string playerLayerName = "Player";
    [SerializeField] Transform pos;
    [SerializeField] Vector2 boxSize;

    Rigidbody2D rig;
    private Animator animator;

    private GameObject AtkObject;

    private float characterScale;
    [SerializeField] private float moveSpeed = 0.4f; //�⺻�̼�
    [SerializeField] private float maxSpeed = 3.5f; // �ִ��̼�
    [SerializeField] float jumpPower = 2.3f; // ����
    private bool isJumping, jump;

    private float curTime;
    private float coolTime = 0.5f;


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
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) //��Ű �ѹ��� ���� �ѹ���
        {
            jump = true;
        }

        if(curTime <= 0) //���� ��Ÿ�� ����
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0); //���ݹ����ȿ� �����Գ� Ȯ��
                foreach( Collider2D collider2D in collider2Ds) //�������� �����
                {
                    if(collider2D.tag == "Enemy")//�����ȿ� ���� hp������ hp���� �۾�
                    {
                        AtkObject = collider2D.gameObject;
                        AtkObject.GetComponent<HPController>().GetDamage(10, transform.position);
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

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayerName),
                                      LayerMask.NameToLayer(oneWayPlatformLayerName),
                                      true);
        }
        else
        {
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer(playerLayerName),
                          LayerMask.NameToLayer(oneWayPlatformLayerName),
                          false);
        }
    }

    private void FixedUpdate()
    {
        //��Ű �� ������ �������� �ϵ��� �߷� ����
        if (!jump && rig.velocity.y>0)
        {
            rig.gravityScale = 2.3f;
        }
        else
        {
            rig.gravityScale = 4.0f;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }

}
