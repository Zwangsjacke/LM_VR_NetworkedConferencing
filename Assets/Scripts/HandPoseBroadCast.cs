using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandPoseBroadCast : MonoBehaviour
{
    public bool foundManager = false;

    public NetworkGameManager networkGameManager;
    public GameManagerScript gameManager;




    public void ThumbsUp()
    {

        {
            FindGameManager();
            if (foundManager)
            {

                gameManager.ThumbsUp();

            }
        }

    }
    public void FindGameManager()
    {
        if (!foundManager)
        {
            try
            {
                networkGameManager = GameObject.FindGameObjectWithTag("networkGameManager").GetComponent<NetworkGameManager>();
                gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManagerScript>();

                Debug.Log("Found my Managers");
            }
            catch (System.Exception)
            {

                throw;
            }

            if (gameManager != null)
            {
                foundManager = true;
            }


        }
    }

}
