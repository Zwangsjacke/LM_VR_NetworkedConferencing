using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Dissonance;

public class WebCamScript : NetworkBehaviour
{
    public GameObject dissonanceHolderOne;
    public GameObject dissonanceHolderTwo;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if(MyNetworkManager.mySingleton.playerNumber == 1)
        {
            Debug.Log("I think, I'm player One");
            dissonanceHolderOne.SetActive(true);
        }
        else if(MyNetworkManager.mySingleton.playerNumber == 2)
        {
            dissonanceHolderTwo.SetActive(true);
            Debug.Log("I think, I'm player Two");
        }
    }
}
