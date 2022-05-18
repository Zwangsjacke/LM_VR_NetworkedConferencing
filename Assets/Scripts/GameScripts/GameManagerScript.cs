using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public MyNetworkManager networkManager;
    public NetworkGameManager networkGameManager;
    public BaseGame[] games;
    public int gameCounter = 0;
    public bool endCondition = false;
    public bool alreadyThumbs = false;

    public string header;
    public string body;

    /// <summary>
    /// Finds GO with Networkmanager Tag
    /// </summary>

    public void Awake()
    {
        FindNetworkedGameManager();
        FindNetworkManager();
    }

    public void FindNetworkedGameManager()
    {
        GameObject go = GameObject.FindGameObjectWithTag("networkGameManager");
        networkGameManager = go.GetComponent<NetworkGameManager>();
    }
    public void FindNetworkManager()
    {
        GameObject go = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = go.GetComponent<MyNetworkManager>();
    }

    public void StartNextGame()
    {
        if (gameCounter <= 3)
        {
        endCondition = false;
        games[gameCounter].StartGame();
        gameCounter++;
        alreadyThumbs = false;
        }
        else
        {
            EndVRStudy();
        }
    }

    public void ThumbsUp()
    {
        if (endCondition && !alreadyThumbs)
        {
            alreadyThumbs = true;
            networkGameManager.CMDThumbsUp();
        }
    }

    public void EndVRStudy()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("GamePrefab"))
        {
            Destroy(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("header"))
        {
            go.SendMessage("ChangeText", header);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("body"))
        {
            go.SendMessage("ChangeText", body);
        }
    }
}
