using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoGame : BaseGame
{

    public override void SpawnObjects()
    {
        networkGameManager.SpawnOneForEach(prefabIds[0],prefabIds[1], SpawnLocationsPlayerOne[0], SpawnLocationsPlayerTwo[0]);
        networkGameManager.SpawnObjects(prefabIds[2], SpawnLocationsPlayerOne[1], SpawnLocationsPlayerTwo[1]);
    }
}
