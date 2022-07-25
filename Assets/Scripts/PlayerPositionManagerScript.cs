using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerPositionManagerScript : NetworkBehaviour
{
    public PlayerRigScript playerRig;

    [SerializeField]
    [SyncVar]
    private int numPlayers = 1;

    public override void OnStartClient()
    {
        base.OnStartClient();
        //if (isServer) return;
        CMDMovingThePlayers(numPlayers);        
    }
    
    /// <summary>
    /// Increments numPlayers and calls MovePlayer on each Client
    /// </summary>
    /// <param name="t"></param>
    [Command(requiresAuthority = false)]
    public void CMDMovingThePlayers(int t)
    {
        Debug.Log("Moving the Players");
        numPlayers++;
        RPCMovePlayer(t);
    }

    /// <summary>
    /// Calls Move on playerRig, because it cant have the NetworkIdentity which Networkbehaviour demands.
    /// </summary>
    /// <param name="numPlayers"></param>
    [ClientRpc]
    public void RPCMovePlayer(int t)
    {
        playerRig.Move(t);
    }
}
