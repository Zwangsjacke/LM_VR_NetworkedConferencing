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
        networkManager.SpawnForBothClients(networkManager.spawnPrefabs[prefabId], spawnLocationOne, spawnLocationTwo);
        networkManager.SpawnForBothClients(networkManager.spawnPrefabs[alarmId], alarmSpawnOne, alarmSpawnTwo);
    }
}
