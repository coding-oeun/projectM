using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Button : MonoBehaviour
{
    // 1회만 눌리게 수정
    public GameManager gameManager;
    public AudioSource clickSound;
    bool isGameEnd;

    private void Start()
    {
        isGameEnd = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            if(!isGameEnd)
            {
                Debug.Log("1회 충돌 처리문 진입");
                Debug.Log("버튼&손 충돌");
                transform.position = transform.position + new Vector3(0, -0.1f, 0);
                isGameEnd = true;
                clickSound.Play();
                StartCoroutine(ButtonReturn());
            }
        }
    }

    public IEnumerator ButtonReturn() // 버튼 눌렀을 때 1초후 제자리 
    {
        Debug.Log("버튼 제자리 코루틴 진입");
        yield return new WaitForSeconds(1f);
        Vector3 buttonReturn = Vector3.up * Mathf.Lerp(0.1f, 0.09f, 0.1f);
        transform.position = transform.position + buttonReturn;
        yield return new WaitForSeconds(1f);
        Debug.Log("게임 클리어 처리");
        isGameEnd = false;
        gameManager.gameClear = true;
        StartCoroutine(gameManager.GameEnd());
    }
}





