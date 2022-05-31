using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockObject : MonoBehaviour
{
    public Rigidbody rigidBody;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            MakeKinematic();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            MakeNotKinematic();
        }
    }

    private void MakeKinematic()
    {
        rigidBody.isKinematic = true;
    }

    private void MakeNotKinematic()
    {
        rigidBody.isKinematic = false;
    }
}
