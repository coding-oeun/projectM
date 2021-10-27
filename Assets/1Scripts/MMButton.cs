using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MMButton : MonoBehaviour
{
    // 1회만 눌리게 수정
    public GameManager gameManager;
    public TextMeshPro musicModeText;

    bool isGameEnd;

    private void Start()
    {
        isGameEnd = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if (!isGameEnd)
            {
                Debug.Log("MMMode: " + gameManager.gameMMMode);
                transform.position = transform.position + new Vector3(0, -0.1f, 0);
                gameManager.gameMMMode += 1;
                isGameEnd = true;

                return; //테스트용으로 여기서 함수 끝

                if (gameManager.gameMMMode == 1)
                {
                    Debug.Log("MMMode: " + gameManager.gameMMMode);
                    musicModeText.color = Color.blue;
                    Debug.Log("TMMode Text UI 출력");
                    gameManager.gameTMMode = 0;
                }
                if (gameManager.gameMMMode == 2)
                {
                    SceneManager.LoadScene("MusicMode");
                }

                StartCoroutine(ButtonReturn());
            }
        }
    }

    public IEnumerator ButtonReturn() // 버튼 눌렀을 때 1초후 제자리 
    {
        Debug.Log("MMMode: " + gameManager.gameMMMode);
        Debug.Log("코루틴진입");
        isGameEnd = false;
        yield return new WaitForSeconds(0.5f);
        Vector3 buttonReturn = Vector3.up * Mathf.Lerp(0.1f, 0.09f, 0.1f);
        transform.position = transform.position + buttonReturn;
    }
}