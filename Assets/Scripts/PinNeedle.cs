using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinNeedle : MonoBehaviour
{
    public Rigidbody rigidBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBoard"))
        {
            SetConstrains();
            Debug.Log("Pinned!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PinBoard"))
        {
            ReleaseConstraints();
            Debug.Log("Unpinned!");
        }
    }

    public void SetConstrains()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        rigidBody.useGravity = false;
        rigidBody.freezeRotation = true;
    }

    public void ReleaseConstraints()
    {
        rigidBody.constraints = RigidbodyConstraints.None;
        rigidBody.useGravity = true;
        rigidBody.freezeRotation = false;
    }
}
