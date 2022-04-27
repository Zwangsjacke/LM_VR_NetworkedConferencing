using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class PlayerRigScript : MonoBehaviour
{
    public Transform spawnPointOne;
    public Transform spawnPointTwo;

    public bool click = false;

    public bool alreadySpawned = false;



    public 
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (click)
        {
            Move(1);
            click = false;
        }
    }



    public void Move(int numPlayers)
    {

        Debug.Log("Move Function called");

        if (alreadySpawned) return;

        alreadySpawned = true;

        Debug.Log($"There are {numPlayers} player connected");

        if(numPlayers == 1)
        {
            this.transform.position = spawnPointOne.position;
            Debug.Log("Spawning as Player One");
        }
        else if (numPlayers == 2)
        {
            this.transform.position = spawnPointTwo.position;
            Debug.Log("Spawning as Player Two");
        }
        else
        {
            Debug.Log("Are you even connected?");
        }


    }
}
