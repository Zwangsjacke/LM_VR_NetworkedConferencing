using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPoseBroadCast : MonoBehaviour
{
    public bool foundManager = false;

    public NetworkGameManager gameManager;




    public void ThumbsUp()
    {

        FindGameManager();
        gameManager.CMDThumbsUp();

    }
    public void FindGameManager()
    {
        if (!foundManager)
        {
            gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<NetworkGameManager>();
            foundManager = true;

        }
    }

}
