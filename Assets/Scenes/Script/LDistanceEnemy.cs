using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LDistanceEnemy : MonoBehaviour
{
    private float distance; //적과 플레이어 거리 계산
    private GameObject enemyRecog; //적이 플레이어를 추적할때 활성화되는 오브젝트
    [SerializeField]LineRenderer rayLine; //적이 플레이어를 추적할때 실시간으로 수정되는 선 긋기
    [SerializeField]LayerMask layerMask; // 선이 플레이어를 마주닿는지 확인하기 위한 마스크
    [SerializeField]Transform targetTransform; // 플레이어 실시간 위치 받아오기
    [SerializeField]GameObject AtkTarget; // 플레이어를 따라가는 표적

    private float lineWidth = 0.06f;
    private Vector2 rayStart, rayEnd;
    private bool recog, Atk = false;

    bool pauseDuration = false; // 멈추는 시간 (초)

    void Start()
    {
        //AtkTarget = GameObject.Find("AtkTarget").GetComponent<LDistanceTarget>();   
        rayStart = new Vector2(transform.position.x, transform.position.y);
        //layerMask = LayerMask.GetMask("Player");
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);
        AtkTarget.SetActive(false);

        // LineRenderer 설정
        rayLine.positionCount = 2; // 두 점으로 직선을 그리므로
        rayLine.startWidth = lineWidth;
        rayLine.endWidth = lineWidth;

        rayLine.material = new Material(Shader.Find("Sprites/Default"));
        rayLine.startColor = Color.blue;
        rayLine.endColor = Color.blue;

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, targetTransform.position);

        if (distance < 4.0f)
        {
            if(!recog)
            {
                enemyRecog.SetActive(true);
                AtkTarget.SetActive(true);
                rayStart = transform.position;
                //rayLine.SetPosition(0, rayStart); // 시작점
                rayLine.positionCount = 2;
                LDistanceTarget.isFollowing = true;
                AtkTarget.transform.Translate(targetTransform.position);
            }

            Atk = LDistanceTarget.isFollowing;
            if (Atk)
            {
                rayLine.SetPosition(0, rayStart); // 시작점
                rayLine.SetPosition(1, AtkTarget.transform.position); // 끝점
                recog = true;

            }
            else
            {
                if (!pauseDuration)
                {
                    LongAtk(rayStart, AtkTarget.transform.position);
                    Invoke("ResumeFollowing", 1.0f);
                }
                else
                {
                    pauseDuration = true;
                }
            }

        }
        else
        {
            if (recog)
            {
                enemyRecog.SetActive(false);
                AtkTarget.SetActive(false);
                rayLine.positionCount = 0;
            }
            recog = false;
        }

    }
    void LongAtk(Vector2 rayStart, Vector2 rayEnd)
    {
        rayLine.positionCount = 0;
        rayLine.startColor = Color.red;
        rayLine.endColor = Color.red;

        rayLine.positionCount = 2;
        rayLine.SetPosition(0, rayStart); // 시작점
        rayLine.SetPosition(1, rayEnd); // 끝점

        RaycastHit2D hit = Physics2D.Raycast(rayStart, rayEnd, 200, layerMask);
        Debug.DrawLine(rayStart, rayEnd, Color.red);

        //curTime = 0.0f;
    }

    void ResumeFollowing()
    {
        rayLine.positionCount = 0;
        rayLine.startColor = Color.blue;
        rayLine.endColor = Color.blue;

        rayLine.positionCount = 2;
    }

}
