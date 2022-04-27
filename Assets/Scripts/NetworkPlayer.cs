using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class NetworkPlayer : MonoBehaviour
{
    public Transform cameraRig;

    public Transform head;
    public Transform leftHand;
    public Transform rightHand;


    // Update is called once per frame

    private void Awake()
    {
        cameraRig = GameObject.FindGameObjectWithTag("CameraRig").transform;

    }
    void Update()
    {
        leftHand.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        rightHand.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        leftHand.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);
        rightHand.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);

        

        head.transform.position = cameraRig.transform.position;
        head.transform.rotation = cameraRig.transform.rotation;
    }

}
