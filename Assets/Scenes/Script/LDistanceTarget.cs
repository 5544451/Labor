using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDistanceTarget : MonoBehaviour
{
    [SerializeField] Transform target; // 따라갈 목표 물체
    [SerializeField] float followSpeed ; // 따라가는 속도
    [SerializeField] float followDuration; // 따라가는 시간 (초)
    [SerializeField] float pauseDuration; // 멈추는 시간 (초)

    private float followTime = 0.0f; // 따라간 시간
    public static bool isFollowing = false; // 따라가고 있는지 여부
    private Vector3 pausePosition; // 멈춘 위치


    void Update()
    {
        if (isFollowing)
        {
            // 목표 물체를 천천히 따라가기
            transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);

            // 따라간 시간 누적
            followTime += Time.deltaTime;

            // 설정한 따라가는 시간이 지나면 따라가기를 멈추고 위치 저장
            if (followTime >= followDuration)
            {
                isFollowing = false; // 따라가기 중지
                pausePosition = transform.position; // 현재 위치 저장
                Invoke("ResumeFollowing", pauseDuration); // 일정 시간 후 다시 따라가기 시작
            }
        }
        else
        {
            // 멈춘 위치에 머무르기
            transform.position = pausePosition;
        }
    }

    // 다시 따라가기 시작
    void ResumeFollowing()
    {
        isFollowing = true;
        followTime = 0.0f; // 시간 초기화
    }

}
