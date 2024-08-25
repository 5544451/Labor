using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_charactor : MonoBehaviour
{
    Rigidbody2D rig;
    private Animator animator;

    private float characterScale;
    private float moveSpeed = 0.4f; //기본이속
    private float maxSpeed = 3.5f; // 최대이속
    private float jumpPower = 2.3f; // 점속
    private bool isJumping, jump;

    // Start is called before the first frame update
    void Start()
    {
        characterScale = transform.localScale.x;
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isJumping = false; //지금 점프하는 중인가? 1단점프만 하도록 설정
        jump = true; //점프가능? 키 한번에 점프 한번만 
    }
    void OnCollisionEnter2D(Collision2D collision) //충돌 감지
    {
        if (collision.gameObject.tag == "Ground")
        { //tag가 Floor인 오브젝트와 충돌했을 때
            isJumping = false; //isJumping을 다시 false로
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))//move_left
        {
            rig.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);  // 물체에 왼쪽 방향으로 물리적 힘을 가해줌
            rig.velocity = new Vector2(Mathf.Max(rig.velocity.x, -maxSpeed), rig.velocity.y); //일정 속도에 도달하면 더 이상 빨라지지 않게함.
            //rig.velocity = new Vector2(-moveSpeed, rig.velocity.y);
            transform.localScale = new Vector3(-characterScale, characterScale, 1);

            //animator.SetBool("moving", true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rig.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);// 물체에 오른쪽 방향으로 물리적 힘을 가해줌.
            rig.velocity = new Vector2(Mathf.Min(rig.velocity.x, maxSpeed), rig.velocity.y); //일정 속도에 도달하면 더 이상 빨라지지 않게함.
            //rig.velocity = new Vector2(moveSpeed, rig.velocity.y);
            transform.localScale = new Vector3(characterScale, characterScale, 1);
            //animator.SetBool("moving", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) // 이동 키를 뗀 경우
        {
            // x 속도를 줄여 이동 방향으로 아주 살짝만 움직이고 거의 바로 멈추게 합니다.
            rig.velocity = new Vector2(rig.velocity.normalized.x, rig.velocity.y);
            //animator.SetBool("moving", false);
        }

        // 위 화살표를 누르면 점프합니다.
        if (Input.GetKey(KeyCode.UpArrow) && !isJumping && jump)
        {
            rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
            jump = false;
            //Debug.Log("jump");
        }

        if (Input.GetKeyUp(KeyCode.UpArrow)) //윗키 한번에 점프 한번만
        {
            jump = true;
        }

    }

}
