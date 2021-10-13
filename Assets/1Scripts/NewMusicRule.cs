using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMusicRule : MonoBehaviour
{
    public GameManager gameManager;
    public VRManager vrManager;
    public AudioSource mainMusic;

    public bool musicStop; //음악 정지 상태 진입 체크용

    public int mapLevel;
    public float stopLevel;
    public float playLevel;
    public bool YellowCheck;

    private void Start()
    {
         mapLevel = 1;
         stopLevel = 3;
         playLevel = 8;
         YellowCheck = false;
    }

    private void Update()
    {
        MusicLevel();
        TrafficLight();
    }

    public void MusicStart()
    {
        Debug.Log("음악시작진입");
        mainMusic.Play();
        StartCoroutine(PlayTime(playLevel));
    }
    
    IEnumerator PlayTime(float playLevel)
    {
        musicStop = false;
        gameManager.checkStopPoint = false;
        gameManager.isBlock = false;
        Debug.Log(musicStop);
        yield return new WaitForSeconds(playLevel); //MusicLevel 참조
        StartCoroutine(YellowLight());
    }

    void MusicPause()
    {
        Debug.Log("음악정지진입");
        mainMusic.Pause();
        StartCoroutine(StopTime(stopLevel));
    }

    IEnumerator StopTime(float stopLevel)
    {
        musicStop = true;
        Debug.Log(musicStop);
        yield return new WaitForSeconds(stopLevel); //MusicLevel 참조
        StartCoroutine(YellowLight());
        
    }

    IEnumerator YellowLight()
    {
        YellowCheck = true;
        Debug.Log("Yellow ON");
        yield return new WaitForSeconds(1.5f);
        YellowCheck = false;
        gameManager.checkStopPoint = false;
        Debug.Log("Yellow OFF");

        if(musicStop == true)
        {
            MusicStart();
        }
        else if(musicStop == false)
        {
            MusicPause();
        }
    }

    void MusicLevel()
    {
        if (mapLevel == 1 && vrManager.HmdCurrentPosCheck.z >= -11)
        {
            mapLevel++;
            Debug.Log(mapLevel);
            playLevel = 7;
            stopLevel = 5;
        }
        else if (mapLevel == 2 && vrManager.HmdCurrentPosCheck.z >= 18)
        {
            mapLevel++;
            Debug.Log(mapLevel);
            playLevel = 5;
            stopLevel = 7;
        }
    }
    void TrafficLight()
    {
        if (YellowCheck == true)
        {
            Debug.Log("Yellow");
        }
        else if (musicStop == true && YellowCheck == false)
        {
            Debug.Log("Red");
        }
        else if (musicStop != true && YellowCheck == false)
        {
            Debug.Log("Green");
        }
    }

}