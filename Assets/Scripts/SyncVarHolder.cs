using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SyncVarHolder : NetworkBehaviour
{
    public PlayerRigScript playerRig;
    public MyNetworkManager networkManager;
    public Transform spawn;

    [SyncVar]
    public int numPlayers = 0;




    public override void OnStartClient()
    {
        base.OnStartClient();


        MovingThePlayers(numPlayers);
        Debug.Log(numPlayers);
        
    }

    [Command(requiresAuthority = false)]
    public void MovingThePlayers(int t)
    {
        numPlayers++;
        MovePlayer(t);
    }

    [ClientRpc]
    public void MovePlayer(int numPlayers)
    {
        playerRig.Move(numPlayers);
    }
}
