using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SocketScript : MonoBehaviour
{
    public MeshRenderer body;
    public MeshRenderer pin;




    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Pinned Photo"))
        {
            body.enabled = true;
            pin.enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pinned Photo"))
        {
            body.enabled = false;
            pin.enabled = false;
        }
    }
}
