using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPositioningScript : MonoBehaviour
{
    public Transform localPosition;
    public GameObject PlayerCameraRig;


    public void Start()
    {
        PositionPlayer();
    }


    [ContextMenu("Calibrate")]
    public void PositionPlayer()
    {
        Vector3 spawnPosition = new Vector3(localPosition.position.x, PlayerCameraRig.transform.position.y, localPosition.position.z);
        Quaternion spawnRotation = new Quaternion(localPosition.rotation.x, localPosition.rotation.y, localPosition.rotation.z,1);

        PlayerCameraRig.transform.rotation = spawnRotation;
        PlayerCameraRig.transform.position = spawnPosition;
    }
}
