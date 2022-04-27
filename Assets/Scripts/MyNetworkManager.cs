using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager

{
    public NetworkConnectionToClient clientOneConn;
    public NetworkConnectionToClient clientTwoConn;

    public Transform firstFotoSpawnLocation;
    public Transform secondFotoSpawnLocation;


    public NetworkConnection[] clientConnections = new NetworkConnection[2];


    private int clientCount = 0;


    // When server is started
    public override void OnStartServer()
    {
        base.OnStartServer();
    }

    // Whenever a client connects:
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);

        StoreClientConn(conn);


        if (clientCount == 2)
        {
            SpawnForBothClients(spawnPrefabs[0], firstFotoSpawnLocation, secondFotoSpawnLocation);
        }

    }



    // Takes a prefab and two spawnLocations. One for each client.
    public void SpawnForBothClients(GameObject prefab, Transform firstSpawnLocation, Transform secondSpawnLocation)
    {

        if (clientConnections[0] == null || clientConnections[1] == null) return;

        Vector3 pos = firstSpawnLocation.position;
        Quaternion rot = firstSpawnLocation.rotation;


        GameObject go1 = Instantiate(prefab, pos, rot);
        NetworkServer.Spawn(go1, clientConnections[0]);

        pos = secondSpawnLocation.position;
        rot = secondSpawnLocation.rotation;

        GameObject go2 = Instantiate(prefab, pos, rot);
        NetworkServer.Spawn(go2, clientConnections[1]);

    }



    // KeyPad uses this to set IP-Address
    public void SetIp(string newIp)
    {
        networkAddress = newIp;
    }

    // Stores Client Conn ID for spawning objects with the correct authority
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




}

