using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using Dissonance.Integrations.MirrorIgnorance;

public class MyNetworkManager : NetworkManager

{
    public GameObject rightHandVisual;
    public GameObject leftHandvisual;
    public OnServnerTracking tracker;
    public ToBlackFader fader;

    [Header("Client Information")]

    private NetworkConnectionToClient clientOneConn;
    private NetworkConnectionToClient clientTwoConn;
    public int clientCount = 0;
    private NetworkConnection[] clientConnections = new NetworkConnection[2];

    [Tooltip("1. In person scene\n2. Videoconference scene")]
    public string[] scenes;

    public int playerNumber;


    public string studyCondition;

    public TMP_InputField inputField;
    public int experimentNumber;
    public bool serverPuffer = true;

    //Singelton
    public static MyNetworkManager mySingleton; 

    public Transform phoneSpawnLocation;

    // We need the server to start as a host, so that he can speak with the players.

    public override void Awake()
    {
        mySingleton = this;
        base.Awake();
    }
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);

        if (serverPuffer)
        {
            serverPuffer = false;
            return;
        }
        StoreClientConn(conn);

    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        DisableHandVisuals();
        fader.Fade(false);
        
    }


    /// <summary>
    /// On player connection set avatars and gamecondition on local networkmanagers
    /// </summary>
    /// <param name="conn"></param>
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {

        base.OnServerAddPlayer(conn);
        foreach (NetworkConnection connection in clientConnections)
        {

            if(connection != null)
            {
                Debug.Log(connection.address);
                connection.identity.GetComponent<NetworkPlayer>().RPCRequestNewInfos();
                connection.identity.GetComponent<NetworkPlayer>().networkManager.studyCondition = studyCondition;
            }   
        }
        tracker.AddPlayer(conn.identity.gameObject);
        if(studyCondition == "VideoConference") Destroy(conn.identity.GetComponent<MirrorIgnorancePlayer>());
    }

    /// <summary>
    /// Spawns prefab for both players and allocates authority accordingly 
    /// </summary>
    /// <param name="prefab">Must be in "spawnables"</param>
    /// <param name="firstSpawnLocation"></param>
    /// <param name="secondSpawnLocation"></param>
    public void SpawnForBothClients(GameObject prefab, Transform firstSpawnLocation, Transform secondSpawnLocation)
    {

        Vector3 pos = firstSpawnLocation.position;
        Quaternion rot = firstSpawnLocation.rotation;

        GameObject go1 = Instantiate(prefab, pos, rot);
        NetworkServer.Spawn(go1, clientConnections[0]);

        pos = secondSpawnLocation.position;
        rot = secondSpawnLocation.rotation;

        GameObject go2 = Instantiate(prefab, pos, rot);
        NetworkServer.Spawn(go2, clientConnections[1]);

    }

    public void SpawnOneForEach(GameObject prefab, GameObject secondPrefab, Transform firstSpawnLocation, Transform secondSpawnLocation)
    {
        Vector3 pos = firstSpawnLocation.position;
        Quaternion rot = firstSpawnLocation.rotation;

        GameObject go1 = Instantiate(prefab, pos, rot);
        NetworkServer.Spawn(go1, clientConnections[0]);

        pos = secondSpawnLocation.position;
        rot = secondSpawnLocation.rotation;

        GameObject go2 = Instantiate(secondPrefab, pos, rot);
        NetworkServer.Spawn(go2, clientConnections[1]);

    }

    /// <summary>
    /// Sets current IP to new IP
    /// </summary>
    /// <param name="newIp"></param>
    public void SetIp(string newIp)
    {
        networkAddress = newIp;
    }

    /// <summary>
    /// Increases the client count by one and stores the connectionsID of the current connection in clientConnections[]
    /// </summary>
    /// <param name="conn"></param>
    public void StoreClientConn(NetworkConnectionToClient conn)
    {
               
        if (clientCount == 0)
        {
            clientOneConn = conn;
            clientConnections[0] = clientOneConn;
            Debug.Log($"Player One connected and Connection stored: {clientOneConn}");
           
        }

        if (clientCount == 1)
        {
            clientTwoConn = conn;
            clientConnections[1] = clientTwoConn;
            Debug.Log($"Player Two connected and Connection stored: {clientTwoConn}");


        }
        if (clientCount > 2)
        {
            Debug.Log("Too many players!");
        }

        clientCount++;

    }


    /// <summary>
    /// Deactivates all GameObjects with the tag HandVisual
    /// </summary>
    public void DisableHandVisuals()
    {
        rightHandVisual.SetActive(false);
        leftHandvisual.SetActive(false);

    }

    public void ChangeOnlineScene(string newScene)
    {
        onlineScene = newScene;
    }

    public void HostInPersonServer()
    {
        if(int.TryParse(inputField.text, out experimentNumber)){

           ChangeOnlineScene(scenes[0]);
           studyCondition = "InPerson";
           StartHost();
        }
        else
        {
            return;
        }
    }

    public void HostVideoConferencingServer()
    {
        if (int.TryParse(inputField.text, out experimentNumber))
        {
            ChangeOnlineScene(scenes[1]);
            studyCondition = "VideoConference";
            StartHost();
        }
        else
        {
            return;
        }
    }

    public void DestroyRoom(int id)
    {
        if(id == 1)
        {
                Destroy(GameObject.Find("Scenery Player Two"));

        }
        else if (id == 2)
        {
                Destroy(GameObject.Find("Scenery Player One"));

        }
        else
        {
            Debug.Log("Player number is wrong");
        }
    }

}

