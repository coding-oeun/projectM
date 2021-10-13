using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public VRManager vrManager;
    public void OnTriggerEnter(Collider other) //콜라이더 충돌 시 이동
    {
        if (other.gameObject.CompareTag("Hand"))
        {
            vrManager.timeCount = 0;

            vrManager.ColliderTouch++;

            if (vrManager.ColliderTouch > 10) //최대 속도 제한
            {
                vrManager.ColliderTouch = 10;
            }

            Debug.Log(vrManager.ColliderTouch);

            Transform playermove = vrManager.player.GetComponent<Transform>(); //이동 스크립트로 이동
            playermove.transform.position = vrManager.Move();

        }
    }
}