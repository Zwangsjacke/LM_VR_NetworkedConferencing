using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertSurvivalGame : BaseGame
{
    public int numPinned = 0;
    public int numPins = 6;


    public override void SetCondition()
    {
        if(numPinned == numPins) base.SetCondition();
    }
}
