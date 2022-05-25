using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGameManager : NetworkBehaviour
{
    public GameManagerScript gameManager;
    public MyNetworkManager networkManager;

    public int thumbsRequired = 2;

    [SyncVar]
    public int numThumbs;


    public void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
        // networkManager = NetworkManager.singleton.GetComponent<MyNetworkManager>();
    }

    public void SpawnObjects(int prefabId, Transform spawnLocationOne, Transform spawnLocationTwo)
    {
        if (isServer)
        {
        networkManager.SpawnForBothClients(networkManager.spawnPrefabs[prefabId], spawnLocationOne, spawnLocationTwo);
        }
    }

    [Command(requiresAuthority = false)]
    public void CMDThumbsUp()
    {
        numThumbs++;
        if(numThumbs == thumbsRequired)
        {
            numThumbs = 0;
            RPCRdyStartNextGame();
        }
    }



    [ClientRpc]
    public void RPCRdyStartNextGame()
    {
        gameManager.StartNextGame();
    }
}
