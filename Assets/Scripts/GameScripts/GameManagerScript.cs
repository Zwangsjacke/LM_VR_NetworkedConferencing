using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public MyNetworkManager networkManager;

    /// <summary>
    /// Finds GO with Networkmanager Tag
    /// </summary>
    public void FindNetworkManager()
    {
        GameObject go = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = go.GetComponent<MyNetworkManager>();
    }

}
