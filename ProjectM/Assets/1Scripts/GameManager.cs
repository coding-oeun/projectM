using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameClear = false;
    public bool gameStart;

    int timer;
    public GameObject readyText;
    public GameObject startText;

    public MusicRule musicRule; // MusicRule Script 호출
    public NewMusicRule newMusicRule;
    public VRManager vrManager;
    public bool movePenalty = false; //  패널티 발생 상태 체크
    Vector3 stopPosition;
    Vector3 currentPosition;

    float penaltyDis = 1.0f;
    private int life; // 플레이어 기회
    public bool checkStopPoint = false; // 멈췄을 때 플레이어 위치체크 여부
    public bool checkPenaltyYn = false;


    void Start()
    {
        // 레디고 출력관련
        gameStart = false;
        readyText.SetActive(false);
        startText.SetActive(false);

 // 생명 3
        life = 3;
    }

    // 1. 게임 시작 준비 (레디고)
    void ReadyGo()
    {
        if (timer <= 200)
        {
            timer++;
            Debug.Log(timer);

            if (timer < 200)
            {
                readyText.SetActive(true);
            }

            if (timer >= 200)
            {
                readyText.SetActive(false);
                startText.SetActive(true);
                StartCoroutine(this.LoadingEnd());
                gameStart = true;
                newMusicRule.mainMusic.Play();
                Debug.Log("gameStart True");
            }

        }

    }

    IEnumerator LoadingEnd()
    {
        yield return new WaitForSeconds(1.0f);
        startText.SetActive(false);
    }

    // 1. VR 기기 움직임 체크
    // 2. 음악상태 체크

    bool CheckVRDevices()
    {
        // 음악이 정지
        if (musicRule.startWait == true)
        {
            Debug.Log("움직임 체크 시작");
            // 1. 정지 순간 위치
            if (checkStopPoint == false)
            {
                stopPosition = vrManager.HmdCurrentPosCheck;
                checkStopPoint = true;
            }

            // 2. 실시간 위치
            currentPosition = vrManager.HmdCurrentPosCheck;

            // 3. 두 위치간 거리
            float hmdPosDis = Vector3.Distance(currentPosition, stopPosition);

            Debug.Log("stop pos : " + stopPosition);
            Debug.Log("curr pos : " + currentPosition);
            Debug.Log("distance : " + hmdPosDis);

            Debug.Log("움직인 거리 : " + hmdPosDis);
            if (hmdPosDis > penaltyDis)
            {
                return true;
            }

            Debug.Log("움직임 체크 끝");
        }

        return false;
    }

    void PenaltyProcess()
    {
        Debug.Log("패널티 시작");
        if (checkPenaltyYn == false)
        {
            life -= 1;
            checkPenaltyYn = true;
            Debug.Log("라이프 1 감소");
            Debug.Log("남은 라이프 : " + life);
        }

        if (musicRule.startWait == false)
        {
            checkPenaltyYn = false;
            checkStopPoint = false;
        }
        Debug.Log("패널티 끝");
    }


    void Update()
    {
        if (gameStart == false)
        {
            ReadyGo();
        }

        // 움직이면 패널티 실행
        if (CheckVRDevices() == true)
        {
            PenaltyProcess();
        }
    }
}