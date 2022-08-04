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
    public GameObject eve;
    public static NetworkPlayer localPlayer;
    public ApperanceHolder apperanceHolder;
    public Apperance apperance;
    public MyNetworkManager networkManager;
    PlayerRigScript singelton;


    private void Awake()
    {

            networkManager = MyNetworkManager.mySingleton;
            apperanceHolder = ApperanceHolder.apperanceHolder;
                    
    }
    private void Start()
    {
        singelton = PlayerRigScript.singleton;

        if (isLocalPlayer)
        {
            localPlayer = this;
            
            CMDSetApparence(apperanceHolder.customs, apperanceHolder.colors);

            //if (isServer)
            //{
            //    Destroy(this.gameObject);
            //}
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
        //leftHand.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LHand);
        //rightHand.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RHand);
        leftHand.transform.SetPositionAndRotation(singelton.leftHand.transform.position, singelton.leftHand.transform.rotation);
        rightHand.transform.SetPositionAndRotation(singelton.rightHand.transform.position, singelton.rightHand.transform.rotation);


        //leftHand.transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LHand);
        //rightHand.transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RHand);

        head.transform.SetPositionAndRotation(singelton.centerEye.transform.position, singelton.centerEye.transform.rotation);
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
