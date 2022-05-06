using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NetworkPlayer : MonoBehaviour
{
    [Header("Transforms of Bodyparts")]

    public Transform cameraRig;
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private void Awake()
    {
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig").transform;
    }

    /// <summary>
    /// Constantly sets position of hands and head.
    /// </summary>
    void Update()
    {

        leftHand.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        rightHand.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        leftHand.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);
        rightHand.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);

        head.transform.SetPositionAndRotation(cameraRig.transform.position, cameraRig.transform.rotation);
    }
}
