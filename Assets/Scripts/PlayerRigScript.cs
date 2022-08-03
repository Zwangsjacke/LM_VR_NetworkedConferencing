using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigScript : MonoBehaviour
{
    public GameObject playerRig;

    [Header("Spawn Points")]
    public Transform spawnPointOneVideo;
    public Transform spawnPointTwoVideo;
    public Transform spawnPointOneInPerson;
    public Transform spawnPointTwoInPerson;

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

        if (MyNetworkManager.singelton.studyCondition == "VideoConference")
        {
            Debug.Log("Moving Player in Condition Video Conference");
        if(numPlayers == 1)
        {
            playerRig.transform.SetPositionAndRotation(spawnPointOneVideo.position, spawnPointOneVideo.rotation);
                //playerRig.transform.position = spawnPointOneVideo.position;
            Debug.Log("Spawning as Player One");
            MyNetworkManager.singelton.playerNumber = 1;
        }
        else if (numPlayers == 2)
        {
            playerRig.transform.SetPositionAndRotation(spawnPointTwoVideo.position, spawnPointTwoVideo.rotation);
                //playerRig.transform.position = spawnPointTwoVideo.position;
            Debug.Log("Spawning as Player Two");
            MyNetworkManager.singelton.playerNumber = 2;
        }
        else
        {
            Debug.Log($"Something went wrong. There cannot be {numPlayers} players.");
        }

        MyNetworkManager.singelton.DestroyRoom(MyNetworkManager.singelton.playerNumber);

        }
        else
        {
            if (numPlayers == 1)
            {
                Debug.Log("Spawning as Player One");
                MyNetworkManager.singelton.playerNumber = 1;
            }
            else if (numPlayers == 2)
            {
                playerRig.transform.RotateAround(spawnPointOneInPerson.transform.position, Vector3.up, 180);                
                playerRig.transform.position += spawnPointTwoInPerson.transform.position - spawnPointOneInPerson.transform.position;
                Debug.Log("Spawning as Player Two");
                MyNetworkManager.singelton.playerNumber = 2;
            }
            else
            {
                Debug.Log($"Something went wrong. There cannot be {numPlayers} players.");
            }
        }

    }
}
