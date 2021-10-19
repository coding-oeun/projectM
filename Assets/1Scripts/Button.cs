using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Button : MonoBehaviour
    {
        public GameManager gameManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Hand"))
            {
                transform.position = transform.position + new Vector3(0, -0.1f, 0);

                gameManager.GameOver();

            }
        }
    }
