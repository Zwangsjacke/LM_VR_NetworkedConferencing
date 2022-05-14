using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame : MonoBehaviour
{
    public MyNetworkManager networkManager;
    public int prefabId;
    public GameManagerScript gameManager;

    [Header("SpawnLocations")]
    public Transform spawnLocationOne;
    public Transform spawnLocationTwo;

    public void Awake()
    {
            FindNetworkManager();     
    }


    public void FindNetworkManager()
    {
        GameObject go = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = go.GetComponent<MyNetworkManager>();
    }

    public virtual void StartGame()
    {
        ClearGamePrefabs();
        networkManager.SpawnForBothClients(networkManager.spawnPrefabs[prefabId], spawnLocationOne, spawnLocationTwo);
    }


    /// <summary>
    /// Destroys GO with GamePrefab Tag
    /// </summary>
    public void ClearGamePrefabs()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("GamePrefab");

        foreach (GameObject go in gos)
        {
            Destroy(go);
        }
    }

}
