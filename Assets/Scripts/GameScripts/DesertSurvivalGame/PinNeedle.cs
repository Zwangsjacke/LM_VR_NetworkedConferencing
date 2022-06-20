using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinNeedle : MonoBehaviour
{
    public DesertSurvivalGame desertSurvival;
    public Transform pinPlane;
    public bool setPosition;

    private void Awake()
    {

        desertSurvival = GameObject.FindGameObjectWithTag("gameManager").GetComponent<DesertSurvivalGame>();
        pinPlane = GameObject.Find("Snapping Plane").transform;
    }

    private void Update()
    {
        if (setPosition)
        {
            Quaternion rot = new Quaternion(0, 0, 0, 0);
            Vector3 pos = new Vector3(pinPlane.position.x, this.transform.position.y, this.transform.position.z);
            transform.SetPositionAndRotation(pos, rot);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBoard"))
        {
            Debug.Log("Pinned!");
            desertSurvival.numPinned++;
            desertSurvival.SetCondition();
            setPosition = true;
        }
        if (other.CompareTag("Hand"))
        {
            setPosition = true;
            Debug.Log("Collided with Hand");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PinBoard"))
        {
            Debug.Log("Unpinned!");
            desertSurvival.numPinned--;
            setPosition = false;           
        }
    }



}
