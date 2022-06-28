using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Mirror;

public class NetworkPlayer : NetworkBehaviour
{
    [Header("Transforms of Bodyparts")]

    public GameObject cameraRig;
    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;


    private void Start()
    {
        if (isLocalPlayer)
        {
            cameraRig = GameObject.FindGameObjectWithTag("Player");
            if (isServer)
            {
                head.SetActive(false);
                leftHand.SetActive(false);
                rightHand.SetActive(false);
                cameraRig.SetActive(false);
                Debug.Log("Destroyed?");
            }
        }
    }


    /// <summary>
    /// Constantly sets position of hands and head.
    /// </summary>
    void Update()
    {
        if (isServer) return;
        if (isLocalPlayer)
        {

            SetPositions();
        }
    }

    public void SetPositions()
    {
        leftHand.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        rightHand.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);

        leftHand.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);
        rightHand.transform.localRotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);

        head.transform.SetPositionAndRotation(cameraRig.transform.position, cameraRig.transform.rotation);
    }
}
