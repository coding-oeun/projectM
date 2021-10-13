using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public GameManager gameManager;
    void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("MusicMode 이동");
        SceneManager.LoadScene("MusicMode");
        gameManager.gameMode = 1;
    }
    //gameMode : 1. musicMode 2.trafficMode

    //public void OnClickMusicMode()
    //{
    //    Debug.Log("MusicMode 이동");
    //    SceneManager.LoadScene("MusicMode");
    //    gameManager.gameMode = 1;
    //}
    //public void OnClickTrafficMode()
    //{
    //    Debug.Log("TrafficMode 이동");
    //    SceneManager.LoadScene("TrafficMode");
    //    gameManager.gameMode = 2;
    //}
}
