using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            transform.position = transform.position + new Vector3(-0.1f, 0, 0);
            
            StartCoroutine(buttonReturn());

        }
    }

    IEnumerator buttonReturn()
    {
        yield return new WaitForSeconds(1f);

        Vector3 softmove = Vector3.right* Mathf.Lerp(0.1f, 0.09f, 0.1f);
        transform.position = transform.position + softmove;


    }
}
