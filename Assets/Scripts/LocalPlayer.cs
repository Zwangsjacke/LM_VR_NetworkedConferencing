using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayer : MonoBehaviour
{
    public static LocalPlayer localPlayer;
    public LocalPlayer localPlayerVar;
    public Apperance apperance;


    private void Awake()
    {
        localPlayerVar = this;

    }
}
