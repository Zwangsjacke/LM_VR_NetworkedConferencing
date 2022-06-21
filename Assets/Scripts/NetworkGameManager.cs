using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGameManager : NetworkBehaviour
{
    public GameManagerScript gameManager;
    public MyNetworkManager networkManager;

    public Transform alarmTransformPlayerOne;
    public Transform alarmTransformPlayerTwo;

    public PhoneScript phone;

    public int thumbsRequired = 2;

    [SyncVar]
    public int numThumbs;


    public void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
        //networkManager.SpawnForBothClients(networkManager.spawnPrefabs[0], alarmTransformPlayerOne, alarmTransformPlayerTwo);
    }

    public void SpawnObjects(int prefabId, Transform spawnLocationOne, Transform spawnLocationTwo)
    {
        if (isServer)
        {
            networkManager.SpawnForBothClients(networkManager.spawnPrefabs[prefabId], spawnLocationOne, spawnLocationTwo);
        }
    }

    public void SpawnOneForEach(int prefabId,int secondPrefabId, Transform spawnLocationOne, Transform spawnLocationTwo)
    {
        if (isServer)
        {
            networkManager.SpawnOneForEach(networkManager.spawnPrefabs[prefabId], networkManager.spawnPrefabs[secondPrefabId], spawnLocationOne, spawnLocationTwo);
        }
    }

    [Command(requiresAuthority = false)]
    public void CMDTurnPhonesOff()
    {
        RPCTurnOffPhone();
    }

    [Command(requiresAuthority = false)]
    public void CMDThumbsUp()
    {
        numThumbs++;
        if(numThumbs == thumbsRequired)
        {
            numThumbs = 0;
            RPCRdyStartNextGame();
            gameManager.StartNextGame();
        }
    }



    [ClientRpc]
    public void RPCTurnOffPhone()
    {
        phone.TurnPhoneOff();
    }
    [ClientRpc]
    public void RPCRdyStartNextGame()
    {
        gameManager.StartNextGame();
    }
}
