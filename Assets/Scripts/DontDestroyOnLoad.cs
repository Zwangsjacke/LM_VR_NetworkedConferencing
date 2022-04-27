using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    // Add this Script to prevent object being destroyed when the scene changes.
    void Start()
    {
        DontDestroyOnLoad(this);
    }


}
