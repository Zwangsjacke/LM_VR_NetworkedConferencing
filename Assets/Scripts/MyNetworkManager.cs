using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager

{

    [Header("Client Information")]

    private NetworkConnectionToClient clientOneConn;
    private NetworkConnectionToClient clientTwoConn;
    private int clientCount = 0;
    private NetworkConnection[] clientConnections = new NetworkConnection[2];

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);

        StoreClientConn(conn);

        DisableHandVisuals();

    }

    /// <summary>
    /// Spawns prefab for both players and allocates authority accordingly 
    /// </summary>
    /// <param name="prefab">Must be in "spawnables"</param>
    /// <param name="firstSpawnLocation"></param>
    /// <param name="secondSpawnLocation"></param>
    public void SpawnForBothClients(GameObject prefab, Transform firstSpawnLocation, Transform secondSpawnLocation)
    {

        Debug.Log("Tried to spawn");

       // if (clientConnections[0] == null || clientConnections[1] == null) return;

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
        clientCount++;

        if (clientCount == 1)
        {
            clientOneConn = conn;
            clientConnections[0] = clientOneConn;
            Debug.Log($"Player One connected and Connection stored: {clientOneConn}");
           
        }

        if (clientCount == 2)
        {
            clientTwoConn = conn;
            clientConnections[1] = clientTwoConn;
            Debug.Log($"Player Two connected and Connection stored: {clientTwoConn}");
        }
        if (clientCount > 2)
        {
            Debug.Log("Too many players!");
        }


    }


    /// <summary>
    /// Deactivates all GameObjects with the tag HandVisual
    /// </summary>
    public void DisableHandVisuals()
    {
        GameObject[] handVisuals = GameObject.FindGameObjectsWithTag("HandVisual");

        foreach(GameObject go in handVisuals)
        {
            go.SetActive(false);
        }

    }

}

