using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LecomotionController : MonoBehaviour
{
    public XRController leftRay;
    public XRController rightRay;
    public InputHelpers.Button RayActivationButton;
    public float activationThreshold = 0.1f;

    void Update()
    {
        if (leftRay)
        {
            leftRay.gameObject.SetActive(CheckIfActivated(leftRay));
        }

        if (rightRay)
        {
            rightRay.gameObject.SetActive(CheckIfActivated(rightRay));
        }
    }

    public bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, RayActivationButton, out bool isActivated);
        return isActivated;
    }

}
