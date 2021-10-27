using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MMButton : MonoBehaviour
{
    // 1회만 눌리게 수정
    public GameManager gameManager;
    public TMButton tMButton;
    public AudioSource clickSound;
    public TextMeshPro musicModeText;
    public int gameMMMode;

    bool isGameEnd;

    private void Start()
    {
        isGameEnd = false;
        gameMMMode = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if (!isGameEnd)
            {
                Debug.Log("TMMode: " + gameMMMode);
                transform.position = transform.position + new Vector3(0, -0.1f, 0);
                gameMMMode += 1;
                clickSound.Play();
                isGameEnd = true;

                return; // 시연용 진입 막기

                if (gameMMMode == 1)
                {
                    Debug.Log("TMMode: " + gameMMMode);
                    musicModeText.color = Color.blue;
                    Debug.Log("TMMode Text UI 출력");
                    tMButton.gameTMMode = 0;
                    tMButton.trafficModeText.color = Color.white;
                }
                if (gameMMMode == 2)
                {
                    SceneManager.LoadScene("TrafficMode");
                }

                StartCoroutine(ButtonReturn());
            }
        }
    }

    public IEnumerator ButtonReturn() // 버튼 눌렀을 때 1초후 제자리 
    {
        Debug.Log("TMMode: " + gameMMMode);
        Debug.Log("코루틴진입");
        yield return new WaitForSeconds(1f);
        Vector3 buttonReturn = Vector3.up * Mathf.Lerp(0.1f, 0.09f, 0.1f);
        transform.position = transform.position + buttonReturn;
        isGameEnd = false;
    }
}