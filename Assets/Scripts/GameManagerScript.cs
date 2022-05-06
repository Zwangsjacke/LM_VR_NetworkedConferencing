using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public MyNetworkManager networkManager;

    [Header("SpawnLocations")]
    public Transform firstPhotoSpawnLocation;
    public Transform secondPhotoSpawnLocation;

    public void Awake()
    {
        FindNetworkManager();
    }

    /// <summary>
    /// Finds GO with Networkmanager Tag
    /// </summary>
    public void FindNetworkManager()
    {
        GameObject go = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = go.GetComponent<MyNetworkManager>();
    }

    /// <summary>
    /// Calls StartGame with according Parameters
    /// </summary>
    public void StartPictureGame()
    {
        StartGame(0, firstPhotoSpawnLocation, secondPhotoSpawnLocation);
    }


    /// <summary>
    /// Spawns Prefabs at given Locations
    /// </summary>
    /// <param name="num">Arrayposition of prefab in Networkmanager.spawnablePrefabs</param>
    /// <param name="firstSpawnLocation"></param>
    /// <param name="secondSpawnLocation"></param>
    public void StartGame(int num, Transform firstSpawnLocation, Transform secondSpawnLocation)
    {
        ClearGamePrefabs();

        networkManager.SpawnForBothClients(networkManager.spawnPrefabs[num], firstSpawnLocation, secondSpawnLocation);
    }

    /// <summary>
    /// Destroys GO with GamePrefab Tag
    /// </summary>
    public void ClearGamePrefabs()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("GamePrefab");

        foreach(GameObject go in gos)
        {
            Destroy(go);
        }
    }

}
