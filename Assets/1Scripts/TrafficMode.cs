using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficMode : MonoBehaviour
{
    public GameManager gameManager;
    public VRManager vrManager;
    public AudioSource mainMusic;

    public int mapLevel;
    public float stopLevel;
    public float playLevel;
    public bool YellowCheck;

    public GameObject redRight;
    public GameObject yellowRight;
    public GameObject greenRight;

    public Renderer redRenderer;
    public Renderer yellowRenderer;
    public Renderer greenRenderer;

    private void Start()
    {
        mapLevel = 1;
        stopLevel = 3;
        playLevel = 8;
        YellowCheck = false;

        redRenderer = GameObject.FindWithTag("RedRight").GetComponent<Renderer>();
        yellowRenderer = GameObject.FindWithTag("YellowRight").GetComponent<Renderer>();
        greenRenderer = GameObject.FindWithTag("GreenRight").GetComponent<Renderer>();
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
        if(gameManager.gameStart == false)
        {
            redRenderer.material.color = Color.red;
            yellowRenderer.material.color = Color.gray;
            greenRenderer.material.color = Color.gray;
        }
        else if(gameManager.gameStart == true)
        {
            if (YellowCheck == true)
            {
                Debug.Log("Yellow");
                yellowRenderer.material.color = Color.yellow;
                redRenderer.material.color = Color.gray;
                greenRenderer.material.color = Color.gray;
            }
            else if (gameManager.musicStop == true && YellowCheck == false)
            {
                Debug.Log("Red");
                redRenderer.material.color = Color.red;
                yellowRenderer.material.color = Color.gray;
                greenRenderer.material.color = Color.gray;
            }
            else if (gameManager.musicStop != true && YellowCheck == false)
            {
                Debug.Log("Green");
                greenRenderer.material.color = Color.green;
                yellowRenderer.material.color = Color.gray;
                redRenderer.material.color = Color.gray;
            }
        }
        
    }

}