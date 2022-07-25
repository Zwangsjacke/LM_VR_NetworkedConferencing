using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class NetworkGameManager : NetworkBehaviour
{
    public GameManagerScript gameManager;
    public MyNetworkManager networkManager;

    public GameObject experimenterNotification;

    [Header("Distractors")]
    public DoorKnockingScript door;
    public PictureScript picture;
    public PhoneScript phoneOne;
    public PhoneScript phoneTwo;
    public BirdScript bird;

    public bool startedDistractorTimer;

    public int thumbsRequired = 2;

    [SyncVar]
    public int numThumbs;



    public void Start()
    {
        networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetworkManager>();
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

    public void DeactivateExperimenterNotification()
    {
        experimenterNotification.SetActive(false);
    }

    
    [Command(requiresAuthority = false)]
    public void NotifyExperimenter()
    {
        experimenterNotification.SetActive(true);
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
            if (startedDistractorTimer) return;
            CMDStartDistractorTimer();
            startedDistractorTimer = true;
        }
    }
    [Command(requiresAuthority = false)]
    public void CMDStartDistractorTimer()
    {
        Debug.Log("Called in CMD");
        RPCStartDistractorTimer();
    }



    [ClientRpc]
    public void RPCTurnOffPhone()
    {
        phoneOne.TurnPhoneOff();
        phoneTwo.TurnPhoneOff();
    }
    [ClientRpc]
    public void RPCRdyStartNextGame()
    {
        gameManager.StartNextGame();
    }
    [ClientRpc]
    public void RPCStartDistractorTimer()
    {
        Debug.Log("Called in RPC");
        door.timerActive = true;
        picture.timerActive = true;
        if (MyNetworkManager.singelton.playerNumber==1)
        {

        phoneOne.timerActive = true;
        }
        else
        {
        phoneTwo.timerActive = true;

        }
        bird.timerActive = true;
    }
}
