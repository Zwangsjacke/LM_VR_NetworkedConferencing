using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigScript : MonoBehaviour
{
    public GameObject playerRig;


    [Header("Spawn Points")]
    public Transform spawnPointOne;
    public Transform spawnPointTwo;

    [Header("Spawn Check")]
    public bool alreadySpawned = false;

    /// <summary>
    /// Moves the CameraRig to a spawnlocation according to the number of connected players.
    /// Should be called, when connecting as a client.
    /// </summary>
    /// <param name="numPlayers"></param>
    public void Move(int numPlayers)
    {

        Debug.Log("Move Function called");

        if (alreadySpawned) return;


        alreadySpawned = true;

        if(numPlayers == 1)
        {
            playerRig.transform.position = spawnPointOne.position;
            Debug.Log("Spawning as Player One");
            MyNetworkManager.singelton.playerNumber = 1;
        }
        else if (numPlayers == 2)
        {
            playerRig.transform.position = spawnPointTwo.position;
            Debug.Log("Spawning as Player Two");
            MyNetworkManager.singelton.playerNumber = 2;
        }
        else
        {
            Debug.Log($"Something went wrong. There cannot be {numPlayers} players.");
        }

        MyNetworkManager.singelton.DestroyRoom(MyNetworkManager.singelton.playerNumber);

    }
}
