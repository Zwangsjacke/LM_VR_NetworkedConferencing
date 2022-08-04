using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class MicroPremissionRequester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Permission.RequestUserPermission(Permission.Microphone);
        Microphone.Start(Microphone.devices[0], false, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
