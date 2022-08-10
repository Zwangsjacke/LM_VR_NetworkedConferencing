using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigScript : MonoBehaviour
{
    public GameObject playerRig;
    public GameObject centerEye;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject rotatePoint;

    public static PlayerRigScript singleton;

    [Header("Spawn Check")]
    public bool alreadySpawned = false;

    private void Start()
    {
        singleton = this;
    }


    /// <summary>
    /// Moves the CameraRig to a spawnlocation according to the number of connected players.
    /// Should be called, when connecting as a client.
    /// </summary>
    /// <param name="numPlayers"></param>
    public void Move(int numPlayers, string cond)
    {

        Debug.Log($"Move Function called.{cond} is the condition");

        if (alreadySpawned) return;


        alreadySpawned = true;

        if (cond == "VideoConference")
        {
            Debug.Log("Moving Player in Condition Video Conference");

            if(numPlayers == 1)
            {
                Debug.Log("Spawning as Player One");
                MyNetworkManager.mySingleton.playerNumber = 1;
            }
            else if (numPlayers == 2)
            {
                Debug.Log("Trying to Move Player down");
                playerRig.transform.position = new Vector3(playerRig.transform.position.x,playerRig.transform.position.y - 22.68f, playerRig.transform.position.z);
                Debug.Log("Movement Happend");
                MyNetworkManager.mySingleton.playerNumber = 2;
            }
            else
            {
                Debug.Log($"Something went wrong. There cannot be {numPlayers} players.");
            }

            MyNetworkManager.mySingleton.DestroyRoom(MyNetworkManager.mySingleton.playerNumber);

        }
        else
        {
            Debug.Log("Spawning Player in InPersonCondition");
            if (numPlayers == 1)
            {

                Debug.Log("Spawning as Player One");
                MyNetworkManager.mySingleton.playerNumber = 1;
            }
            else if (numPlayers == 2)
            {
                playerRig.transform.RotateAround(rotatePoint.transform.position, Vector3.up, 180);                
                Debug.Log("Spawning as Player Two");
                MyNetworkManager.mySingleton.playerNumber = 2;
            }
            else
            {
                Debug.Log($"Something went wrong. There cannot be {numPlayers} players.");
            }
        }

    }


}
