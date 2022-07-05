using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApperanceHolder : MonoBehaviour
{
    public int[] customs;
    public Color[] colors;
    public static ApperanceHolder apperanceHolder;

    private void Awake()
    {
        apperanceHolder = this;
    }
    public void SaveApperance(int[] newCustoms)
    {
        customs = newCustoms;
    }

    public void SaveColor(Color[] newColors)
    {
        colors = newColors;
    }
}
