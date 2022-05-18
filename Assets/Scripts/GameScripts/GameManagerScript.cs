using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    public MyNetworkManager networkManager;
    public NetworkGameManager networkedGameManager;
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
        GameObject go = GameObject.FindGameObjectWithTag("gameManager");
        networkedGameManager = go.GetComponent<NetworkGameManager>();
    }
    public void FindNetworkManager()
    {
        GameObject go = GameObject.FindGameObjectWithTag("NetworkManager");
        networkManager = go.GetComponent<MyNetworkManager>();
    }

    public void StartNextGame()
    {
        if (endCondition && gameCounter <= 2)
        {
        games[gameCounter].StartGame();
        gameCounter++;
        endCondition = false;
        alreadyThumbs = false;
        }
        if(endCondition && gameCounter == 3)
        {
            EndVRStudy();
        }
    }

    public void ThumbsUp()
    {
        if (endCondition && !alreadyThumbs)
        {
            alreadyThumbs = true;
            networkedGameManager.CMDThumbsUp();
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
