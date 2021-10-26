using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MMButton : MonoBehaviour
{
    // 1회만 눌리게 수정
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            transform.position = transform.position + new Vector3(0, -0.1f, 0);
            gameManager.gameMMMode += 1;
            if(gameManager.gameMMMode == 1)
            {
                Debug.Log("MMMode Text UI 출력");
                gameManager.gameTMMode = 0;
            }
        }
    }
}