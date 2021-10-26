using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TMButton : MonoBehaviour
{
    // 1회만 눌리게 수정
    public GameManager gameManager;
    public GameObject trafficModeText;

    private void Start()
    {
        trafficModeText.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            transform.position = transform.position + new Vector3(0, -0.1f, 0);
            gameManager.gameTMMode += 1;
         
            if (gameManager.gameTMMode == 1)
            {
                trafficModeText.SetActive(true);
                Debug.Log("TMMode Text UI 출력");
                gameManager.gameMMMode = 0;
            }

            StartCoroutine(ButtonReturn());
        }
    }

    public IEnumerator ButtonReturn() // 버튼 눌렀을 때 1초후 제자리 
    {
        yield return new WaitForSeconds(1f);
        Vector3 buttonReturn = Vector3.up * Mathf.Lerp(0.1f, 0.09f, 0.1f);
        transform.position = transform.position + buttonReturn;
    }
}