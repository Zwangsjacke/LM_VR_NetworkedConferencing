using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchCallibration : MonoBehaviour
{
    public static CouchCallibration singleton;

    [Header("Refferences")]
    [SerializeField] private Transform localPlayerRig;

    [Header("Controller positions")]
    [SerializeField] private Transform leftCouchCorner;
    [SerializeField] private Transform rightCouchCorner;
    [SerializeField] private Transform leftController;
    [SerializeField] private Transform rightController;

    [Header("Settings")]
    [SerializeField] private float maxDriftDistance = 0.1F; // Amount in Meters the controler is allowed to drift while callibrating. Directly controlls accuracy. 

    [Header("Changing")]
    [SerializeField] private Vector3 posLeft;
    [SerializeField] private Vector3 posRight;
    private bool callibrating;
    private float calibratecooldown;

    private void Awake()
    {
        singleton = this;
    }

    void Update()
    {
        bool leftTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5F;
        bool rightTrigger = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5F;



        if (!callibrating)
        {
            if (leftTrigger && rightTrigger)
            {
                callibrating = true;
                Debug.Log("Calibriere 60 mal die Sekunde");
                calibratecooldown = 3;
            }
        }
        else
        {
            float leftDist = Vector3.Distance(leftController.position, posLeft);
            float rightDist = Vector3.Distance(rightController.position, posRight);

            if (leftDist >= maxDriftDistance)
            {
                posLeft = leftController.position;
            }
            if (rightDist >= maxDriftDistance)
            {
                posRight = rightController.position;
            }

            if (leftDist < maxDriftDistance && rightDist < maxDriftDistance && leftTrigger && rightTrigger)
            {
                calibratecooldown -= Time.deltaTime;
                if (calibratecooldown <= 0)
                {
                    StartCoroutine(nameof(Callibrate));
                    callibrating = false;
                }
            }
            else
            {
                calibratecooldown = 3;
                callibrating = false;

                return;
            }
        }
    }

    [ContextMenu("Calibrate")]
    private void EditorStartCalibrate()
    {
        StartCoroutine(nameof(Callibrate));
    }
    private IEnumerator Callibrate()
    {
        localPlayerRig.position = Vector3.zero;
        localPlayerRig.rotation = Quaternion.identity;

        Vector3 RoomDir = rightCouchCorner.position - leftCouchCorner.position; // should be zero
        Vector3 ControllerDir = rightController.position - leftController.position;
        localPlayerRig.eulerAngles = new Vector3(0,Quaternion.FromToRotation(ControllerDir, RoomDir).eulerAngles.y,0);
        
        yield return new WaitForSeconds(0.1F); // Dmit die position erst nach rotation berechnet wird

        Vector3 leftPositionOffset = leftController.position - leftCouchCorner.position;
        Vector3 rightPositionOffset = rightController.position - rightCouchCorner.position;
        localPlayerRig.transform.position = -Vector3.Lerp(leftPositionOffset, rightPositionOffset, 0.5F);

        //debugText.text = Math.Round(ControllerDir.magnitude - RoomDir.magnitude,2).ToString();
        FinishSetup();
    }

    public void FinishSetup()
    {
        GetComponent<AudioSource>().Play();

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (leftCouchCorner)
        {
            Gizmos.DrawSphere(leftCouchCorner.position, 0.1F);
        }
        Gizmos.color = Color.green;
        if (rightCouchCorner)
        {
            Gizmos.DrawSphere(rightCouchCorner.position, 0.1F);
        }


        Gizmos.color = Color.red/2F;
        if (leftController)
        {
            Gizmos.DrawSphere(leftController.position, 0.1F);
        }
        Gizmos.color = Color.green / 2F;
        if (rightController)
        {
            Gizmos.DrawSphere(rightController.position, 0.1F);
        }
    }
}
