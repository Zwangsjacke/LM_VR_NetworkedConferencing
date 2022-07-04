using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CalibrationScript : MonoBehaviour
{
    public Transform anker;
    public Vector3 ankerPosition;
    public Quaternion ankerRotation;
    public Transform ankerPos;
    public GameObject OVRCamerarig;
    public int presses;



    private void Update()
    {


        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            if(presses == 0)
            {
                anker.position = ankerPos.position;
                anker.rotation = ankerPos.rotation;

                presses++;
            }
            if(presses == 1)
            {

                Calibrate();
                Debug.Log("Pressed the button");
            }
        }
    }


    [ContextMenu("Calibration")]
    public void Calibrate()
    {
        ankerPosition = new Vector3(anker.position.x, OVRCamerarig.transform.position.y, anker.position.z);
        ankerRotation = new Quaternion(OVRCamerarig.transform.rotation.x, anker.rotation.y, OVRCamerarig.transform.rotation.z, 0);
        OVRCamerarig.transform.rotation = ankerRotation;
        OVRCamerarig.transform.position = ankerPosition;

    }
}
