using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHandColor : MonoBehaviour
{
    public Renderer renderer;
    public Apperance eve;
    public Color[] allAppearenceColors;

    [ContextMenu("ChangeColor")]
    public void ChangeColor()
    {
        GetSkinColor();
        renderer = GetComponent<Renderer>();
        if(allAppearenceColors[4] != null)
        {
            renderer.material.color = allAppearenceColors[4];
            return;
        }
        Debug.Log("Failed Changing Handcolor");

    }

    public void GetSkinColor()
    {
        allAppearenceColors = eve.GetColors();
    }


}
