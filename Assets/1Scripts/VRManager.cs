using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManager : MonoBehaviour
{
    // 게임매니저 변수
    public GameManager gameManager;
    // 플레이어를 담을 변수
    public GameObject player;
    [Header("Device")]
    //헤드셋 위치 변수
    public GameObject hmdPos;
    // 오른쪽 컨트롤러 
    public GameObject rightController;
    // 왼쪽 컨트롤러
    public GameObject leftController;
    Vector3 hmdCurrentPosCheck;
    Vector3 controllerR_CurPosCheck;
    Vector3 controllerL_CurPosCheck;
    Vector3 controllerR_CurPos;
    Vector3 controllerL_CurPos;
    //헤드셋 위치 실시간 체크용(player 위치)
    public Vector3 HmdCurrentPosCheck { get { return hmdCurrentPosCheck; } }
    // 오른쪽 컨트롤러 실시간 위치
    public Vector3 ControllerR_CurPosCheck { get { return controllerR_CurPosCheck; } }
    // 왼쪽 컨트롤러 실시간 위치
    public Vector3 ControllerL_CurPosCheck { get { return controllerL_CurPosCheck; } }

    void Start()
    {
        player = GameObject.Find("XR Rig");
    }
    void Update()
    {
        // 스턴상태가 아닐때만 움직임
        if (gameManager.isBlock != true)
        {
            // 업데이트에서 컨트롤러 메소드 실행 
            ShakeToMove();
        }
        // 실시간 HMD 위치체크하는 메소드 실행 
        DeviceMoveCheck();
        // 업다운 못하게 고정하는 메소드 실행
        UpdownFixed();
    }
    private void LateUpdate()
    {
        ControllerCurPos();
    }
    // 레이트업데이트에서의 컨트롤러 위치
    void ControllerCurPos()
    {
        // 오른쪽 컨트롤러 다음 프레임 위치
        controllerR_CurPos = rightController.transform.position;
        // 왼쪽 컨트롤러 다음 프레임 위치
        controllerL_CurPos = leftController.transform.position;
    }
    // 플레이어는 보는방향으로 이동하는데 위나 아래로 이동을 막는 메소드
    void UpdownFixed()
    {
        player.transform.position = new Vector3(player.transform.position.x, 1, player.transform.position.z);
    }
    //패널티를 위한 VR 기기 움직임 체크
    void DeviceMoveCheck()
    {
        hmdCurrentPosCheck = hmdPos.transform.position;
        controllerR_CurPosCheck = rightController.transform.position;
        controllerL_CurPosCheck = leftController.transform.position;
    }
    // 현재 위치에서 x좌표값0.1 만큼 +
    public Vector3 PlayerMove()
    {
        Vector3 viewingDir = hmdPos.transform.forward * Mathf.Lerp(0.1f, 0.09f, 0.1f);
        return player.transform.position = player.transform.position + viewingDir;
    }

    // 업데이트 위치와 레이트업데이트 위치가 다를때 이동
    void ShakeToMove()
    {
        Vector3 controllerR_StopPos = rightController.transform.position;
        float rightPosComparison = Vector3.SqrMagnitude(controllerR_StopPos - controllerR_CurPos);
        if (rightPosComparison > 0.004f)
        {
            PlayerMove();
        }
        Vector3 controllerL_StopPos = leftController.transform.position;
        float leftPosComparison = Vector3.SqrMagnitude(controllerL_StopPos - controllerL_CurPos);
        if (leftPosComparison > 0.004f)
        {
            PlayerMove();
        }
    }
}
