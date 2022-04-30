using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    /// <summary>
    /// Prevents Object to be destroyed when changing scenes.
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
