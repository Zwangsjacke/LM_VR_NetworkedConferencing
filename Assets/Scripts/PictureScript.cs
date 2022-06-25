using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PictureScript : MonoBehaviour
{
    public Rigidbody rigidBody;
    public AudioSource audioSource;

    public float secondsTillFall;
    public bool timerActive;

    private void Update()
    {
        if (timerActive)
        {
            secondsTillFall -= Time.deltaTime;
            if (secondsTillFall <= 0) FallDown();
        }
    }
    public void FallDown()
    {
        rigidBody.useGravity = true;
        timerActive = false;
    }

    public void MakeSound()
    {
        audioSource.Play();        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            MakeSound();
        }
    }
}
