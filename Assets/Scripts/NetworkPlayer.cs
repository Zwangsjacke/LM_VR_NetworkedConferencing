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
    public static NetworkPlayer localPlayer;
    public ApperanceHolder apperanceHolder;
    public Apperance apperance;
    public MyNetworkManager networkManager;

    private void Awake()
    {
            networkManager = MyNetworkManager.singelton;
            apperanceHolder = ApperanceHolder.apperanceHolder;
            cameraRig = GameObject.FindGameObjectWithTag("CenterEye");
    }
    private void Start()
    {
        if (isLocalPlayer)
        {
            localPlayer = this;
            
            CMDSetApparence(apperanceHolder.customs, apperanceHolder.colors);
            
            //if (isServer)
            //{
            //    head.SetActive(false);
            //    leftHand.SetActive(false);
            //    rightHand.SetActive(false);
            //    cameraRig.SetActive(false);
            //    Debug.Log("Destroyed?");
            //}
        }
    }


    /// <summary>
    /// Constantly sets position of hands and head.
    /// </summary>
    void Update()
    {
        //if (isServer) return;
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

    public void ApplyCustom(int[] customs, Color[] colors)
    {
        for (int i = 0; i < customs.Length; i++)
        {
            apperance.SetApperance(i, customs[i]);
            apperance.SetColor(i, colors[i]);
        }
    }

    [Command]
    public void CMDSetApparence(int[] customs, Color[] colors)
    {
        RPCSetApperance(customs, colors);
    }

    [ClientRpc]
    public void RPCRequestNewInfos()
    {
        CMDSetApparence(apperanceHolder.customs, apperanceHolder.colors);
    }


    [ClientRpc]
    public void RPCSetApperance(int[] customs, Color[] colors)
    {
        ApplyCustom(customs, colors);
        Debug.Log("Tried to Set Apperance");
    }
}
