using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoGame : BaseGame
{
    public Transform tabletSpawnOne;
    public Transform tabletSpawnTwo;
    public int tabletId;
    public override void StartGame()
    {
        ClearGamePrefabs();
        ChangeGameText();
        networkManager.SpawnForBothClients(networkManager.spawnPrefabs[prefabId], spawnLocationOne, spawnLocationTwo);
        networkManager.SpawnForBothClients(networkManager.spawnPrefabs[tabletId], tabletSpawnOne, tabletSpawnTwo);
    }
}
