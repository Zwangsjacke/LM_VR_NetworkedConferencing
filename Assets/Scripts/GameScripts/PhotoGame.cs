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
        networkGameManager.SpawnObjects(prefabId, spawnLocationOne, spawnLocationTwo);
        networkGameManager.SpawnObjects(tabletId, tabletSpawnOne, tabletSpawnTwo);
    }
}
