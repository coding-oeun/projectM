using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManager : MonoBehaviour
{
    [Header("Movement")]
    public float ColliderTouch; //컨트롤러로 콜라이더 터치 카운트값,  move함수 관여
    public bool isTimeLimit; // TimeLimit 코루틴 중복 방지 변수
    public float timeCount; // 이동속도 패널티 방지 변수
    public GameObject player; // 플레이어를 담을 변수

    [Header("Device")]
    public GameObject hmdPos; //헤드셋 위치 변수
    public Vector3 hmdCurrentPosCheck; //헤드셋 위치 실시간 체크용(player 위치)
    public Vector3 hmdPosStop; //패널티 지점 헤드셋 위치 체크용
    float hmdPosDis;
    public GameObject rightController; // 오른쪽 컨트롤러 
    public GameObject leftController;  // 왼쪽 컨트롤러
    Vector3 rightCurrentPos;    // 오른쪽 컨트롤러 현재 프레임 위치 변수
    Vector3 rightCurrentPos2;   // 오른쪽 컨트롤러 다음 프레임 위치 변수
    Vector3 leftCurrentPos;     // 왼쪽 컨트롤러 현재 프레임 위치 변수
    Vector3 leftCurrentPos2;    // 왼쪽 컨트롤러 다음 프레임 위치 변수

    void Start()
    {
        player = GameObject.Find("XR Rig");
        ColliderTouch = 0f;
        isTimeLimit = false;
    }

    void Update()
    {
        MoveMent();

        hmdCurrentPosCheck = hmdPos.transform.position;
        player.transform.position = new Vector3(player.transform.position.x, 1, player.transform.position.z); // 위아래 움직임 방지
        
        timeCount += Time.deltaTime;
        if (timeCount > 5 && isTimeLimit == false)
        {
            StartCoroutine(TimeLimit());
        }
    }

    private void LateUpdate()
    {
        rightCurrentPos2 = rightController.transform.position; // 다음프레임위치
        leftCurrentPos2 = leftController.transform.position; 

    }

    public Vector3 HmdCurrentPosCheck
    {
        get
        {
            return hmdCurrentPosCheck;
        }
    }

    public Vector3 Move()
    {
        float maximum = 2.1f;
        float minimum = 0.1f;

        if (ColliderTouch >= 5) // 증가된 속도
        {

            
            Vector3 softmove = hmdPos.transform.forward * Mathf.Lerp(minimum, maximum, 0.1f); // 움직일때 변경되는 이동값 (서서히 증가, 감소 효과)
            return player.transform.position = player.transform.position + softmove;
        }
        else // 기본 속도
        {

            minimum = 0.1f;
            maximum = 0.09f;

            Vector3 softmove = hmdPos.transform.forward * Mathf.Lerp(minimum, maximum, 0.1f); // 기본 이동값
            return player.transform.position = player.transform.position + softmove;
        }

    }
    IEnumerator TimeLimit() // 5초가 지나면 ColliderTouch 감소
    {
        isTimeLimit = true;
        yield return new WaitForSeconds(0.1f);
        ColliderTouch -= 0.1f; // 

        if (ColliderTouch < 0) // 0 이하 방지
            ColliderTouch = 0;

        if (timeCount > 5)
        {
            StartCoroutine(TimeLimit());
        }
        else
        {
            isTimeLimit = false;
        }
    }

    void MoveMent()
    {
        rightCurrentPos = rightController.transform.position; // 업데이트에서 계속 초기화 하면서 지금위치와 다음프레임위치가 다를때 이동발생
        float rightPosCompare = Vector3.SqrMagnitude(rightCurrentPos - rightCurrentPos2);
        if (rightPosCompare > 0.001f)
        {
            Move();
        }
        leftCurrentPos = leftController.transform.position; // 업데이트에서 계속 초기화 하면서 지금위치와 다음프레임위치가 다를때 이동발생
        float leftPosCompare = Vector3.SqrMagnitude(leftCurrentPos - leftCurrentPos2);
        if (leftPosCompare > 0.001f)
        {
            Move();
        }
    }
}
