using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnockingScript : MonoBehaviour
{
    public AudioSource audioSource;

    public float secondsTillFall;
    public bool timerActive;

    private void Update()
    {
        if (timerActive)
        {
            secondsTillFall -= Time.deltaTime;
            if (secondsTillFall <= 0) Knocking();
        }
    }

    public void Knocking()
    {
        audioSource.Play();
        timerActive = false;
    }
}
