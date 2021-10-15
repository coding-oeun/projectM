using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMode : MonoBehaviour
{
    public GameManager gameManager;
    public VRManager vrManager;

    public AudioSource mainMusic;
    public AudioClip[] musicLevel1; //레벨1
    public AudioClip[] musicLevel2; //레벨2
    public AudioClip[] musicLevel3; //레벨3

    float[] stopTime = { 1.0f, 2.0f, 3.0f, 4.0f };

    public bool musicStop; //음악 정지 상태 진입 체크용

    public int mapLevel;
    public float stopLevel;
    public float playLevel;

    void Start()
    {
        mapLevel = 1;
    }

    void Update()
    {
        MusicLevel();
    }

    void MusicLevel()
    {
        if (mapLevel == 1 && vrManager.HmdCurrentPosCheck.z >= -11)
        {
            mapLevel++; //2
            Debug.Log(mapLevel);
        }
        else if (mapLevel == 2 && vrManager.HmdCurrentPosCheck.z >= 18)
        {
            mapLevel++; //3
            Debug.Log(mapLevel);
        }
    }

    public void MusicStart()
    {
        Debug.Log("음악시작진입");

        if (mapLevel == 1)
        {
            Debug.Log(mapLevel + " 음악클립출력");
            mainMusic.clip = musicLevel1[Random.Range(0, musicLevel1.Length)];
            mainMusic.Play();
        }
        else if (mapLevel == 2)
        {
            Debug.Log(mapLevel + " 음악클립출력");
            mainMusic.clip = musicLevel2[Random.Range(0, musicLevel2.Length)];
            mainMusic.Play();
        }
        else if (mapLevel == 3)
        {
            Debug.Log(mapLevel + " 음악클립출력");
            mainMusic.clip = musicLevel3[Random.Range(0, musicLevel3.Length)];
            mainMusic.Play();
        }

        StartCoroutine(PlayTime());
    }

    IEnumerator PlayTime()
    {
        Debug.Log("PlayTime 진입");
        musicStop = false;
        gameManager.checkStopPoint = false;
        gameManager.isBlock = false;
        yield return new WaitForSeconds(mainMusic.clip.length); //클립 노래 길이만큼 정지
        StartCoroutine(StopTime());
    }

    IEnumerator StopTime()
    {
        musicStop = true;
        // 랜덤범위를 maplevel 에 따라 0~2/3~5/6~8 로 변경해야함
        yield return new WaitForSeconds(stopTime[Random.Range(0, 3)]); //노래 정지
        gameManager.checkStopPoint = false;
        MusicStart();
    }

}
