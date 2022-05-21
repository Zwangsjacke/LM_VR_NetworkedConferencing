using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterviewGame : BaseGame
{
    public Transform alarmSpawnOne;
    public Transform alarmSpawnTwo;
    public int alarmId;
    public override void StartGame()
    {
        ClearGamePrefabs();
        ChangeGameText();
        networkGameManager.SpawnObjects(prefabId, spawnLocationOne, spawnLocationTwo);
        networkGameManager.SpawnObjects(alarmId, alarmSpawnOne, alarmSpawnTwo);
    }
}
