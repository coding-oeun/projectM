using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{

    //버튼 1회 눌리면 모드 음악 출력
    //버튼 2회 눌리면 모드 진입

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
        gameManager.gameMMMode = 2;
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
