using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterviewGame : BaseGame
{
    public override void StartGame()
    {
        base.StartGame();
        SetCondition();
    }
    //Endcondition is handled by the alarm
}
