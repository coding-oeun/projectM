using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MusicRule : MonoBehaviour
{
    public VRManager vrManager;

    // 음악 클립 호출
    public AudioSource musicSource;
    public AudioClip[] musicRule1; //레벨1
    public AudioClip[] musicRule2; //레벨2
    public AudioClip[] musicRule3; //레벨3

    // 음악정지배열
    // lv1 0 ~ 2, lv2 3 ~ 6, lv3 7 ~ 9
    float[] wtime = new float[10];

    // 술래 호출
    public GameObject tagger;

    // 술래 위치 변수
    public Vector3 taggerPos;

    // 술래&플레이어 거리
    float dis;

    // 거리에 따른 레벨 변수
    float disLv1 = 36f;
    float disLv2 = 23f;
    float disLv3 = 10f;

    // 음악 정지 상태 체크
    public bool startWait = false;

    // 업데이트 진입 가능 체크
    public bool updateEnter = true;

    void Start()
    {

    }
    void Update()
    {

        if (updateEnter == true) // 업데이트 진입
        {
            //vrManager.stop = true;

            // 술래 음악 거리 조건에 따른 레벨 랜덤 재생
            if (dis >= disLv1) // 술래&플레이어 거리가 거리레벨보다 멀 때(클 때)
            {
                updateEnter = false; // update 집입 차단
                musicSource.clip = musicRule1[Random.Range(0, musicRule1.Length)];
                musicSource.PlayOneShot(musicRule1[Random.Range(0, musicRule1.Length)]);
                StartCoroutine(PlayPoint1()); // Lv 1 음악 재생 코루틴 진입
                //Debug.Log("Lv 1 음악 클립 랜덤 재생");
                //Lv 1 음악 클립 랜덤 재생
            }
            else
            if (dis >= disLv2) // 술래&플레이어 거리가 거리레벨보다 멀 때(클 때)
            {
                updateEnter = false; // update 집입 차단
                musicSource.clip = musicRule2[Random.Range(0, musicRule2.Length)];
                musicSource.PlayOneShot(musicRule2[Random.Range(0, musicRule2.Length)]);
                StartCoroutine(WaitPoint2()); // Lv2 코루틴 진입
                //Debug.Log("Lv 2 음악 클립 랜덤 재생");
                //Lv 2 음악 클립 랜덤 재생
            }
            else
            if (dis >= disLv3) // 술래&플레이어 거리가 거리레벨보다 멀 때(클 때)
            {
                updateEnter = false; // update 집입 차단
                musicSource.clip = musicRule3[Random.Range(0, musicRule3.Length)];
                musicSource.PlayOneShot(musicRule3[Random.Range(0, musicRule3.Length)]);
                StartCoroutine(WaitPoint3()); // Lv3 코루틴 진입
               //Debug.Log("Lv 3 음악 클립 랜덤 재생");
                //Lv 3 음악 클립 랜덤 재생
            }
        }
    }

    // 음악 재생 코루틴 리스트, PlayPoint 1~3
    IEnumerator PlayPoint1()
    {
        yield return new WaitForSeconds(musicSource.clip.length);
        StartCoroutine(WaitPoint1()); // Lv1 대기 코루틴 진입
    }
    IEnumerator PlayPoint2()
    {
        yield return new WaitForSeconds(musicSource.clip.length);
        StartCoroutine(WaitPoint2()); // Lv2 대기 코루틴 진입
    }
    IEnumerator PlayPoint3()
    {
        yield return new WaitForSeconds(musicSource.clip.length);
        StartCoroutine(WaitPoint3()); // Lv3 대기 코루틴 진입
    }

    // 음악 정지 코루틴 리스트, WaitPoint 1~3
    IEnumerator WaitPoint1()
    {
        startWait = true;
        //Debug.Log("음악정지시작");
        yield return new WaitForSeconds(wtime[Random.Range(0, 3)]);
        //Debug.Log("술래&유저" + dis);
        updateEnter = true; // update 집입 허가
        startWait = false;
        //Debug.Log("음악정지종료/업데이트진입허가");
    }
    IEnumerator WaitPoint2()
    {
        startWait = true;
       // Debug.Log("음악정지시작");
        yield return new WaitForSeconds(wtime[Random.Range(4, 6)]);
       // Debug.Log("술래&유저");
        updateEnter = true; // update 집입 허가
        startWait = false;
      //  Debug.Log("음악정지종료");
    }
    IEnumerator WaitPoint3()
    {
        startWait = true;
       // Debug.Log("음악정지시작");
        yield return new WaitForSeconds(wtime[Random.Range(7, 9)]);
       // Debug.Log("술래&유저");
        updateEnter = true; // update 집입 허가
        startWait = false;
      //  Debug.Log("음악정지종료");
    }
}
