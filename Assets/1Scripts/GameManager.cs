using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 남은 기회 UI 출력
    // 타이머 (선택)
    // 성공 화면 처리
    // 실패 화면 처리
    // 패널티 화면 처리

    //public int gameMode; //1. musicMode 2.trafficMode
    public int gameMMMode;
    public int gameTMMode;
    public bool gameStart;
    public bool gameClear;
    public bool gameOver;
    public float startTime;
    public float nowTime;
    public GameObject readyText;
    public GameObject startText;
    public GameObject clearText;
    public GameObject overText;
    public GameObject tryText;
    public Text lifeText;

    //public MusicRule musicRule; // MusicRule Script 호출
    public TrafficMode trafficMode;
    public MusicMode musicMode;
    public bool musicStop;


    public VRManager vrManager;
    public bool movePenalty = false; //  패널티 발생 상태 체크
    Vector3 stopPosition;
    Vector3 currentPosition;

    Vector3 controllerR_StopPos;
    Vector3 controllerR_CurPos;

    Vector3 controllerL_StopPos;
    Vector3 controllerL_CurPos;


    float penaltyDis = 1.0f;
    private int life; // 플레이어 기회
    public bool checkStopPoint; // 멈췄을 때 플레이어 위치체크 여부
    public bool checkPenaltyYn;
    public bool isBlock;
    
    public Button button;

    //효과음 관리
    public AudioSource effectSound;
    public AudioClip failSound; //패널티 시
    public AudioClip clearSound; //게임 성공 시
    public AudioClip overSound; //게임 실패 시
    void Start()
    {
        //test 용
        gameTMMode = 0;
        gameMMMode = 0;
        //gameTMMode = 0;

        if (gameMMMode == 2)
        {
            musicMode = GameObject.Find("MusicMode").GetComponent<MusicMode>();
        }
        else if (gameTMMode == 2)
        {
            trafficMode = GameObject.Find("TrafficMode").GetComponent<TrafficMode>();
        }

        vrManager = GameObject.Find("VRManager").GetComponent<VRManager>();
        isBlock = true;
        startTime = Time.time;

        // 레디고 출력관련
        gameStart = false;
        startText.SetActive(false);
        ReadyGo();

        life = 1;// test 용으로 1로 값 수정
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeText.text = life + "/ 3";

        gameClear = false;
        clearText.SetActive(false);
        overText.SetActive(false);
        tryText.SetActive(false);
    }

    // 1. 게임 시작 준비 (레디고)
    void ReadyGo()
    {
        Debug.Log("ReadyGo Start");
        StartCoroutine(LoadingEnd());
    }
    IEnumerator LoadingEnd() // Ready -> Go 순차 출력 코루틴
    {
        Debug.Log("StartLoadingEnd ");
        yield return new WaitForSeconds(3);
        readyText.SetActive(false);
        startText.SetActive(true);
        yield return new WaitForSeconds(1);
        startText.SetActive(false);
        gameStart = true;
        Debug.Log("gameStart True");

        isBlock = false;
        Debug.Log("MusicStart");
        //게임 모드 조건문 필요

        if (gameMMMode == 2)
        {
            musicMode.MusicStart(); //기본모드진입
        }
        else if (gameTMMode == 2)
        {
            trafficMode.mainMusic.playOnAwake = true;
            trafficMode.MusicStart(); //신호등모드진입
        }
    }
    void Update()
    {
        nowTime = Time.time - startTime; // nowTime = 현재 게임 경과 시간
                                         //Debug.Log("nowTime: " + nowTime);

        CheckVRDevices();
        //Debug.Log("스턴확인" + isBlock);
    }

    // 1. VR 기기 움직임 체크
    // 2. 음악상태 체크

    void CheckVRDevices()
    {
        // 음악이 정지
        if (musicStop == true)
        {
            Debug.Log("VR디바이스체크");
            // 1. 정지 순간 위치
            if (checkStopPoint == false)
            {
                stopPosition = vrManager.HmdCurrentPosCheck; //음악 정지 상태에서 플레이어 위치 저장
                controllerR_StopPos = vrManager.ControllerR_CurPosCheck;
                controllerL_StopPos = vrManager.ControllerL_CurPosCheck;

                checkStopPoint = true;
            }

            // 2. 실시간 위치
            currentPosition = vrManager.HmdCurrentPosCheck;
            controllerR_CurPos = vrManager.ControllerR_CurPosCheck;
            controllerL_CurPos = vrManager.ControllerL_CurPosCheck;

            // 3. 두 위치간 거리
            float hmdPosDis = Vector3.Distance(currentPosition, stopPosition);
            float ControllerR_PosDis = Vector3.Distance(controllerR_CurPos, controllerR_StopPos);
            float ControllerL_PosDis = Vector3.Distance(controllerL_CurPos, controllerL_StopPos);

            if (ControllerR_PosDis > penaltyDis && isBlock == false)
            {
                Debug.Log("움직임감지");
                PenaltyProcess();
            }
        }
    }
    void PenaltyProcess()
    {
        Debug.Log("패널티 진입");
        effectSound.clip = failSound;
        effectSound.Play();
        isBlock = true;
        life -= 1;
        lifeText.text = life + "/ 3";
        Debug.Log("Life: " + life);

        if (life <= 0)
        {
            Debug.Log("라이프 0 진입");
            Debug.Log("Life: " + life);
            gameOver = true;
            StartCoroutine(GameEnd());
        }
    }


    public IEnumerator GameEnd()
    {
        gameStart = false;

        if (gameClear == true)
        {
            Debug.Log("게임 클리어");
            effectSound.clip = clearSound;
            effectSound.Play();
            clearText.SetActive(true);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("LobbyScene");
        }
        else if (gameOver == true)
        {
            overText.SetActive(true);
            yield return new WaitForSeconds(2);
            effectSound.clip = overSound;
            effectSound.Play();
            overText.SetActive(false);
            tryText.SetActive(true);
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("LobbyScene");
        }
    }
}