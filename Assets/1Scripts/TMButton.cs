using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TMButton : MonoBehaviour
{
    // 1회만 눌리게 수정
    public GameManager gameManager;
    public MMButton mMButton;
    public AudioSource tMSound;
    public AudioClip click;
    public AudioClip tMMusic;
    public TextMeshPro trafficModeText;
    public int gameTMMode;

    bool isGameEnd;

    private void Start()
    {
        isGameEnd = false;
        gameTMMode = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if (!isGameEnd)
            {
                Debug.Log("TMMode: " + gameTMMode);
                transform.position = transform.position + new Vector3(0, -0.1f, 0);
                gameTMMode += 1;
                tMSound.clip = click;
                tMSound.Play();
                isGameEnd = true;

                if (gameTMMode == 1)
                {
                    Debug.Log("TMMode: " + gameTMMode);
                    trafficModeText.color = Color.blue;
                    Debug.Log("TMMode Text UI 출력");
                    tMSound.clip = tMMusic;
                    tMSound.Play();
                    mMButton.gameMMMode = 0;
                    mMButton.musicModeText.color = Color.white;
                }

                StartCoroutine(ButtonReturn());
            }
        }
    }

    public IEnumerator ButtonReturn() // 버튼 눌렀을 때 1초후 제자리 
    {
        Debug.Log("TMMode: " + gameTMMode);
        Debug.Log("코루틴진입");
        yield return new WaitForSeconds(1f);
        Vector3 buttonReturn = Vector3.up * Mathf.Lerp(0.1f, 0.09f, 0.1f);
        transform.position = transform.position + buttonReturn;
        isGameEnd = false;

        if (gameTMMode == 2)
        {
            tMSound.Pause();
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("TrafficMode");
        }
    }
}