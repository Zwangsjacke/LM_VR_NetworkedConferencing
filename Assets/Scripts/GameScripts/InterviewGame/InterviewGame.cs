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
    public override void SpawnObjects()
    {
        networkGameManager.SpawnOneForEach(prefabIds[0], prefabIds[1], SpawnLocationsPlayerOne[0], SpawnLocationsPlayerTwo[0]);
    }
    //Endcondition is handled by the alarm
}
