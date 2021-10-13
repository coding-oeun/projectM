using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMusicRule : MonoBehaviour
{

    // 1. 음악 재생
    // 2. 플레이어 위치에 따라 레벨 결정
    // 3. 레벨에 따라 음악 N초 정지
    // 4. 정지 해제 후 다시 정지에 진입까지 N초동안 블럭

    // 1. 음악 시작 (맵별로 음악은 1개 존재)
    // 2. 음악 정지


    // 정지 시작/종료 상태 체크하는 값 필요

    // 음악정지레벨배열
    //lv1 0 ~ 10, lv2 11 ~ 20, lv3 21 ~ 30
    public GameManager gameManager;
    public VRManager vrManager;
    public AudioSource mainMusic;

    bool musicStop = false; //음악 정지 상태 진입 체크용
    int musicLv = 0;

    private void Start()
    {
        mainMusic.Pause();
    }

    private void Update()
    {

    }

    void MusicStart()
    {
        mainMusic.Play();
        musicStop = false;
        StartCoroutine(PlayTime());
        MusicPause();
    }

    IEnumerator PlayTime()
    {
        Debug.Log("코루틴진입");
        yield return new WaitForSeconds(5);
        mainMusic.Pause();
        
    }

    void MusicPause()
    {
        musicStop = true;
        mainMusic.Pause();
        StartCoroutine(StopLevel());
        MusicStart();
    }

    IEnumerator StopLevel()
    {
        yield return new WaitForSeconds(3);
    }

    void MusicLevel()
    {
        // 플레이어 위치 레벨에 따라 정지 리스트 내 랜덤 출력 
        // if(플레이어 위치 Z 값이 -10 이상, 18 이하일 경우)
        // 레벨 2 정지 리스트 출력
        // if(플레이어 위치 Z값이 18 이상일 경우)
        // 레벨 3 정지 리스트 출력
        // else 레벨 1 정지 리스트 출력

        //if (new Vector3(0,0,18) > vrManager.hmdCurrentPosCheck > new Vector3(0,0,-10))
        //{
        //    musicLv = 2;
        //}
        //else
        //{
        //    musicLv = 1;
        //}
    }
    // 3. 신호등
    // 음악이 시작하면 신호등은 초록색
    // 음악이 정지 상태에서 신호등은 붉은색
    // 음악이 시작/정지하기 1초전에 신호등은 주황색



}
