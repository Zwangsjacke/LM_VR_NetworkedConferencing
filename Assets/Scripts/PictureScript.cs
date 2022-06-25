using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureScript : MonoBehaviour
{
    public Rigidbody rigidBody;
    public AudioSource audioSource;
    

    public void FallDown()
    {
        rigidBody.useGravity = true;
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
