using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketInteractor : MonoBehaviour
{
    public AudioSource audioSource;
    public DesertSurvivalGame survivalGame;

    private void Awake()
    {
        survivalGame = GameObject.FindGameObjectWithTag("gameManager").GetComponent<DesertSurvivalGame>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            PlaceInSocket(other.gameObject);
            survivalGame.IsPinned(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            survivalGame.IsPinned(false);
        }
    }

    public void PlaceInSocket(GameObject socket)
    {
        transform.SetPositionAndRotation(socket.transform.position, socket.transform.rotation);
        audioSource.Play();
    }


}
