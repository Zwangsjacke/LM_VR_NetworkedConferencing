using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SocketInteractor : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject socket;
    public bool pinned;
    public float coolDown = 1;
    public bool rePinnable = true;



    private void Update()
    {
        if (!rePinnable)
        {
            coolDown -= Time.deltaTime;
            if(coolDown <= 0)
            {
                rePinnable = true;
                coolDown = 1;
            }
        }

        if (socket == null || !pinned) return;
        PlaceInSocket(socket);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Socket") && rePinnable)
        {
            socket = other.gameObject;
            pinned = true;
            audioSource.Play();

        }
        if(pinned && other.CompareTag("Hand"))
        {
            Debug.Log("Hand Kontakt");
            rePinnable = false;
            pinned = false;
        }
    }





    public void PlaceInSocket(GameObject pinnedSocket)
    {
        transform.SetPositionAndRotation(pinnedSocket.transform.position, pinnedSocket.transform.rotation);
    }


}
