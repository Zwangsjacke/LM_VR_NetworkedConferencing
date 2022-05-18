using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGame : BaseGame
{
    public override void StartGame()
    {
        base.StartGame();
        gameManager.endCondition = true;
    }
}
