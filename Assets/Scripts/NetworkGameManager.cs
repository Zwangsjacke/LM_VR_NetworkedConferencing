using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkGameManager : NetworkBehaviour
{
    public GameManagerScript gameManager;

    public int thumbsRequired = 2;

    [SyncVar]
    public int numThumbs;


    [Command(requiresAuthority = false)]
    public void CMDThumbsUp()
    {
        numThumbs++;
        if(numThumbs == thumbsRequired)
        {
            numThumbs = 0;
            RPCRdyStartNextGame();
        }
    }


    [ClientRpc]
    public void RPCRdyStartNextGame()
    {
        gameManager.StartNextGame();
    }
}
