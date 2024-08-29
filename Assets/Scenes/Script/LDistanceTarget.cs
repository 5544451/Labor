using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDistanceTarget : MonoBehaviour
{
    [SerializeField] Transform target; // ���� ��ǥ ��ü
    [SerializeField] float followSpeed ; // ���󰡴� �ӵ�
    [SerializeField] float followDuration; // ���󰡴� �ð� (��)
    [SerializeField] float pauseDuration; // ���ߴ� �ð� (��)

    private float followTime = 0.0f; // ���� �ð�
    public static bool isFollowing = false; // ���󰡰� �ִ��� ����
    private Vector3 pausePosition; // ���� ��ġ


    void Update()
    {
        if (isFollowing)
        {
            // ��ǥ ��ü�� õõ�� ���󰡱�
            transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

            // ���� �ð� ����
            followTime += Time.deltaTime;

            // ������ ���󰡴� �ð��� ������ ���󰡱⸦ ���߰� ��ġ ����
            if (followTime >= followDuration)
            {
                isFollowing = false; // ���󰡱� ����
                pausePosition = transform.position; // ���� ��ġ ����
                Invoke("ResumeFollowing", pauseDuration); // ���� �ð� �� �ٽ� ���󰡱� ����
            }
        }
        else
        {
            // ���� ��ġ�� �ӹ�����
            transform.position = pausePosition;
        }
    }

    // �ٽ� ���󰡱� ����
    void ResumeFollowing()
    {
        isFollowing = true;
        followTime = 0.0f; // �ð� �ʱ�ȭ
    }

}
