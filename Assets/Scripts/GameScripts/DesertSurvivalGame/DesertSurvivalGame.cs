using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertSurvivalGame : BaseGame
{
    public int numPinned = 0;
    public int numPins;


    public override void SetCondition()
    {
        if(numPinned == numPins) base.SetCondition();
    }
}
