using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertSurvivalGame : BaseGame
{
    public int numPinned = 0;
    public int numPins = 6;


    public override void StartGame()
    {
        base.StartGame();
        SetCondition();
    }
    //public void SetCondition(bool condition)
    //{

    //    gameManager.endCondition = condition;
    //}

    //public void IsPinned(bool increase)
    //{

    //    if (increase)
    //    {
    //        numPinned++;
    //    }
    //    else
    //    {
    //        numPinned--;
    //    }

    //    text.text = numPinned.ToString();
    //    SetCondition();

    //}
}
