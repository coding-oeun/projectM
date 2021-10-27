using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TMButton : MonoBehaviour
{
    // 1회만 눌리게 수정
    public GameManager gameManager;
    public TextMeshPro trafficModeText;

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
                Debug.Log("TMMode: " + gameManager.gameTMMode);
                transform.position = transform.position + new Vector3(0, -0.1f, 0);
                gameManager.gameTMMode += 1;
                isGameEnd = true;

                if (gameManager.gameTMMode == 1)
                {
                    Debug.Log("TMMode: " + gameManager.gameTMMode);
                    trafficModeText.color = Color.blue;
                    Debug.Log("TMMode Text UI 출력");
                    gameManager.gameMMMode = 0;
                }
                if (gameManager.gameTMMode == 2)
                {
                    SceneManager.LoadScene("TrafficMode");
                }

                StartCoroutine(ButtonReturn());
            }
        }
    }

    public IEnumerator ButtonReturn() // 버튼 눌렀을 때 1초후 제자리 
    {
        Debug.Log("TMMode: " + gameManager.gameTMMode);
        Debug.Log("코루틴진입");
        isGameEnd = false;
        yield return new WaitForSeconds(0.5f);
        Vector3 buttonReturn = Vector3.up * Mathf.Lerp(0.1f, 0.09f, 0.1f);
        transform.position = transform.position + buttonReturn;
    }
}