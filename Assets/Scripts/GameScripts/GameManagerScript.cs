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

    /// <summary>
    /// Finds GO with Networkmanager Tag
    /// </summary>
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
    }

    public void ThumbsUp()
    {
        if (endCondition && !alreadyThumbs)
        {
            alreadyThumbs = true;
            networkedGameManager.CMDThumbsUp();
        }
    }

}
