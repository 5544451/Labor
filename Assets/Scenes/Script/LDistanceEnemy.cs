using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class LDistanceEnemy : MonoBehaviour
{
    private float distance; //���� �÷��̾� �Ÿ� ���
    private GameObject enemyRecog; //���� �÷��̾ �����Ҷ� Ȱ��ȭ�Ǵ� ������Ʈ
    [SerializeField]LineRenderer rayLine; //���� �÷��̾ �����Ҷ� �ǽð����� �����Ǵ� �� �߱�
    [SerializeField]LayerMask layerMask; // ���� �÷��̾ ���ִ���� Ȯ���ϱ� ���� ����ũ
    [SerializeField]Transform targetTransform; // �÷��̾� �ǽð� ��ġ �޾ƿ���
    [SerializeField]GameObject AtkTarget; // �÷��̾ ���󰡴� ǥ��

    private float lineWidth = 0.06f;
    private Vector2 rayStart, rayEnd;
    private bool recog, Atk = false;

    bool pauseDuration = false; // ���ߴ� �ð� (��)

    void Start()
    {
        //AtkTarget = GameObject.Find("AtkTarget").GetComponent<LDistanceTarget>();   
        rayStart = new Vector2(transform.position.x, transform.position.y);
        //layerMask = LayerMask.GetMask("Player");
        enemyRecog = transform.GetChild(0).gameObject;
        enemyRecog.SetActive(false);
        AtkTarget.SetActive(false);

        // LineRenderer ����
        rayLine.positionCount = 2; // �� ������ ������ �׸��Ƿ�
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
                //rayLine.SetPosition(0, rayStart); // ������
                rayLine.positionCount = 2;
                LDistanceTarget.isFollowing = true;
                AtkTarget.transform.Translate(targetTransform.position);
            }

            Atk = LDistanceTarget.isFollowing;
            if (Atk)
            {
                rayLine.SetPosition(0, rayStart); // ������
                rayLine.SetPosition(1, AtkTarget.transform.position); // ����
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
        rayLine.SetPosition(0, rayStart); // ������
        rayLine.SetPosition(1, rayEnd); // ����

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
