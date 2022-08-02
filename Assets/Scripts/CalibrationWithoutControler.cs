using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationWithoutControler : MonoBehaviour
{
    public Transform anchor;
    public GameObject OVRCameraRig;
    void Start()
    {
        RelocatePlayer();
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Registered Button Press");

            RelocatePlayer();
        }
    }

    public void RelocatePlayer()
    {
        //Vector3 ankerPosition = new Vector3(anchor.position.x, OVRCameraRig.transform.position.y, anchor.position.z);
        //Quaternion ankerRotation = new Quaternion(OVRCameraRig.transform.rotation.x, anchor.rotation.y, OVRCameraRig.transform.rotation.z, 1);
        //OVRCameraRig.transform.rotation = ankerRotation;
        //OVRCameraRig.transform.position = ankerPosition;
        OVRCameraRig.transform.position = new Vector3(anchor.position.x, OVRCameraRig.transform.position.y, anchor.transform.position.z);
        Debug.Log("Relocated");
    }
}
