using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_moving : MonoBehaviour
{
    Rigidbody2D rig;
    GameObject enemyRecog;
    private float characterScale;
    private float moveSpeed = 0.3f; //기본이속
    private float maxSpeed = 7; // 최대이속
    private float jumpPower = 20; // 점속

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
        //플레이어가 어디있는지 (좌,우  확인)
        //플레이어 방향으로 이동 + 플레이어와의 거리 계산 반복


    }

/*    IEnumerator Move(Rigidbody2D rig, float moveSpeed)
    {
        float remainDistance = (transform.position - tragetTransform.position).sqrMagnitude;


    }*/
}
