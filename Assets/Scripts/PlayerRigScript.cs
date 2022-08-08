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
    public void Move(int numPlayers)
    {

        Debug.Log("Move Function called");

        if (alreadySpawned) return;


        alreadySpawned = true;

        if (MyNetworkManager.mySingleton.studyCondition == "VideoConference")
        {
            Debug.Log("Moving Player in Condition Video Conference");

            if(numPlayers == 1)
            {
                Debug.Log("Spawning as Player One");
                MyNetworkManager.mySingleton.playerNumber = 1;
            }
            else if (numPlayers == 2)
            {
    
                playerRig.transform.position += new Vector3(0,0, -4.217367f);
                Debug.Log("Spawning as Player Two");
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
