using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinNeedle : MonoBehaviour
{
    public Rigidbody rigidBody;
    public DesertSurvivalGame desertSurvival;
    public Transform lockedPosition;

    private void Awake()
    {
        desertSurvival = GameObject.FindGameObjectWithTag("gameManager").GetComponent<DesertSurvivalGame>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PinBoard"))
        {
            SetConstrains();
            Debug.Log("Pinned!");
            desertSurvival.numPinned++;
            SavePosition();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PinBoard"))
        {
            ReleaseConstraints();
            Debug.Log("Unpinned!");
            desertSurvival.numPinned--;
           
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PinBoard"))
        {
            this.transform.position = new Vector3(transform.position.x, lockedPosition.transform.position.y, lockedPosition.transform.position.z);
        }
    }

    /// <summary>
    /// Freezes the rotation, gravity and y and z transforms
    /// </summary>
    public void SetConstrains()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        rigidBody.useGravity = false;
        rigidBody.freezeRotation = true;
    }

    /// <summary>
    /// Unfreezes everything
    /// </summary>
    public void ReleaseConstraints()
    {
        rigidBody.constraints = RigidbodyConstraints.None;
        rigidBody.useGravity = true;
        rigidBody.freezeRotation = false;
    }

    public void SavePosition()
    {
        lockedPosition = this.transform;
    }
}
