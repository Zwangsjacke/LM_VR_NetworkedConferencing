using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterviewGame : BaseGame
{
    [Header("Alarm Spawn")]
    public int alarmId;
    public Transform alarmSpawnOne;
    public Transform alarmSpawnTwo;

    /// <summary>
    /// Additionally spawns the alarm
    /// </summary>
    public override void StartGame()
    {
        base.StartGame();
        SetCondition();
        networkGameManager.SpawnObjects(alarmId, alarmSpawnOne, alarmSpawnTwo);
    }

    //Endcondition is handled by the alarm
}
