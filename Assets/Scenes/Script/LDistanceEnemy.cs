using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LDistanceEnemy : MonoBehaviour
{
    private float distance;
    private GameObject enemyRecog;
    [SerializeField]LineRenderer rayLine;
    [SerializeField]LayerMask layerMask;
    [SerializeField]Transform targetTransform;
    [SerializeField]GameObject chaseObject;
    private Vector2 pausePosition; // 멈춘 위치

    private float curTime = 0.0f;
    private float timeToChase = 1.5f;
    private float timeToRay = 1.0f;
    private float lineWidth = 0.06f;
    public float followSpeed = 2.0f; // 따라가는 속도

    private Vector2 rayStart, rayEnd;

    private bool recog, chase = false;

        // Start is called before the first frame update
    void Start()
    {
        //chaseObject = GetComponent<GameObject>();   
        rayStart = new Vector2(transform.position.x, transform.position.y);
        //layerMask = LayerMask.GetMask("Player");
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);

        // LineRenderer 설정
        rayLine.positionCount = 0; // 두 점으로 직선을 그리므로
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
                rayLine.positionCount = 2;
                //정찰종료
                //nvokeRepeating("MoveRayPoint", 0f, 0.5f);
                rayStart = transform.position;
                //플레이어를 따라가는 빈 오브젝트 움직임이 정상적이지 않아서 고쳐야함
                //빔쏘는게 안됨
                chaseObject.transform.Translate(targetTransform.position);
                rayLine.SetPosition(0, rayStart);
 

                chase = true;
            }

            if (chase)
            {
                chaseObject.transform.position = Vector3.Lerp(chaseObject.transform.position, targetTransform.position, followSpeed * Time.deltaTime);

                rayEnd = chaseObject.transform.position;
                rayLine.SetPosition(1, rayEnd);
                curTime += Time.deltaTime;

                if (curTime >= timeToChase)
                {
                    pausePosition = chaseObject.transform.position;

                    chase= false;
                    rayLine.positionCount = 0;
                    rayLine.startColor = Color.red;
                    rayLine.endColor = Color.red;

                    rayLine.SetPosition(0, rayStart);
                    rayLine.SetPosition(1, pausePosition); // 끝점
                    Invoke("LongAtk", timeToRay);
                }
            }  

            recog = true;

        }
        else
        {
            if (recog)
            {
                enemyRecog.SetActive(false);
            }
            recog = false;
        }

    }
    void LongAtk()
    {
        rayLine.positionCount = 0;
        rayLine.startColor = Color.blue;
        rayLine.endColor = Color.blue;

        RaycastHit2D hit = Physics2D.Raycast(rayStart, pausePosition, 200, layerMask);
        Debug.DrawLine(rayStart, pausePosition, Color.red);

        chase = true;
        curTime = 0.0f;
    }
}
