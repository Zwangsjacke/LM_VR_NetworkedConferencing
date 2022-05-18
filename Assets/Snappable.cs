using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Snappable : MonoBehaviour

{
    public GameObject snappedTo;
    public bool snapped;


    private void OnTriggerEnter(Collider other)
    {
        if(other == other.CompareTag("snappingPoint"))
        {
            snappedTo = other.gameObject;
            snapped = true;
        }
    }

    private void Update()
    {
        if (snapped)
        {
        this.transform.position = snappedTo.transform.position;
        this.transform.rotation = snappedTo.transform.rotation;

        }
    }
}
