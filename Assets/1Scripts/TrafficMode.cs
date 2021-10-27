using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficMode : MonoBehaviour
{
    public GameObject readyText;
    public GameObject startText;

    public GameManager gameManager;
    public VRManager vrManager;
    public AudioSource mainMusic;

    public int mapLevel;
    public float stopLevel;
    public float playLevel;
    public bool YellowCheck;

    GameObject[] yellowLights;
    GameObject[] redLights;
    GameObject[] greenLights;

    private void Start()
    {
        // 레디고 출력관련
        gameManager.gameStart = false;
        startText.SetActive(false);

        mapLevel = 1;
        stopLevel = 3;
        playLevel = 8;
        YellowCheck = false;

        yellowLights = GameObject.FindGameObjectsWithTag("YellowLight");
        redLights = GameObject.FindGameObjectsWithTag("RedLight");
        greenLights = GameObject.FindGameObjectsWithTag("GreenLight");
        // FindWithTag 는 하이라키상 가장 하단에 있는 오브젝트를 찾는다
        // 모든 태그를 찾는다 FindGameObjectsWithTag

        ReadyGo();
    }

    private void Update()
    {
        MusicLevel();
        TrafficLight();

        if(gameManager.gameStart == false)
        {
            mainMusic.Pause();
        }
    }

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
        gameManager.gameStart = true;
        Debug.Log("gameStart True");

        gameManager.isBlock = false;
        Debug.Log("MusicStart");

        mainMusic.playOnAwake = true;
        MusicStart(); //신호등모드진입

        //if (gameMMMode == 2)
        //{
        //    musicMode.MusicStart(); //기본모드진입
        //}
    }

    public void MusicStart()
    {
        if(gameManager.gameStart == true)
        {
            Debug.Log("음악시작진입");
            mainMusic.Play();
            StartCoroutine(PlayTime(playLevel));
        }
    }
    
    IEnumerator PlayTime(float playLevel)
    {
        gameManager.musicStop = false;
        gameManager.checkStopPoint = false;
        gameManager.isBlock = false;
        Debug.Log(gameManager.musicStop);
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
        gameManager.musicStop = true;
        Debug.Log(gameManager.musicStop);
        yield return new WaitForSeconds(stopLevel); //MusicLevel 참조
        StartCoroutine(YellowLight());
        
    }

    public IEnumerator YellowLight()
    {
        YellowCheck = true;
        Debug.Log("Yellow ON");
        yield return new WaitForSeconds(1.5f);
        YellowCheck = false;
        gameManager.checkStopPoint = false;
        Debug.Log("Yellow OFF");

        if(gameManager.musicStop == true)
        {
            MusicStart();
        }
        else if(gameManager.musicStop == false)
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
            foreach (GameObject yellowLight in yellowLights)
            {
                yellowLight.GetComponent<MeshRenderer>().material.color = Color.yellow;
            }
            foreach (GameObject redLight in redLights)
            {
                redLight.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
            foreach (GameObject greenLight in greenLights)
            {
                greenLight.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
        }
        else if (gameManager.musicStop == true && YellowCheck == false || gameManager.gameStart == false)
        {
            Debug.Log("Red");
            foreach (GameObject redLight in redLights)
            {
                redLight.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            foreach (GameObject greenLight in greenLights)
            {
                greenLight.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
            foreach (GameObject yellowLight in yellowLights)
            {
                yellowLight.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
        }
        else if (gameManager.musicStop != true && YellowCheck == false)
        {
            Debug.Log("Green");
            foreach (GameObject greenLight in greenLights)
            {
                greenLight.GetComponent<MeshRenderer>().material.color = Color.green;
            }
            foreach (GameObject redLight in redLights)
            {
                redLight.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
            foreach (GameObject yellowLight in yellowLights)
            {
                yellowLight.GetComponent<MeshRenderer>().material.color = Color.gray;
            }
        }
    }

}