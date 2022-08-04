using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertConditionCheck : MonoBehaviour
{
    public SocketInteractor[] socketInteractors;

    public DesertSurvivalGame desertSurvivalGame;


    private void Awake()
    {
        desertSurvivalGame = GameManagerScript.singleton.GetComponent<DesertSurvivalGame>();
    }
    private void Update()
    {
        if (CheckIfAllTrue())
        {
            desertSurvivalGame.SetCondition(true);
        }
        else
        {
            desertSurvivalGame.SetCondition(false);
        }

    }

    public bool CheckIfAllTrue()
    {
        for (int i = 0; i < socketInteractors.Length; i++)
        {
            SocketInteractor socket = socketInteractors[i];
            if (!socket.pinned)
            {
                Debug.Log($"Die Socket Nr {i} ist falsch");
                return false;
            }
        }
        return true;
    }
}
