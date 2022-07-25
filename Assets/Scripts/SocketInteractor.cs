using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SocketInteractor : MonoBehaviour
{
    public AudioSource audioSource;
    public DesertSurvivalGame survivalGame;
    public GameObject socket;
    public bool pinned;
    public float coolDown = 1;
    public bool rePinnable = true;

    private void Awake()
    {
        survivalGame = GameObject.FindGameObjectWithTag("gameManager").GetComponent<DesertSurvivalGame>();
    }

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
        if (!pinned) return;
        PlaceInSocket(socket);

        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Socket") && rePinnable)
        {
            socket = other.gameObject;
            pinned = true;
            audioSource.Play();
            survivalGame.IsPinned(true);
        }
        if(pinned && other.CompareTag("Hand"))
        {
            Debug.Log("Hand Kontakt");
            rePinnable = false;
            pinned = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            survivalGame.IsPinned(false);
        }
    }

    public void PlaceInSocket(GameObject pinnedSocket)
    {
        transform.SetPositionAndRotation(pinnedSocket.transform.position, pinnedSocket.transform.rotation);
    }


}
