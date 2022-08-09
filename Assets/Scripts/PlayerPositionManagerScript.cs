using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerPositionManagerScript : NetworkBehaviour
{
    public PlayerRigScript playerRig;

    public static PlayerPositionManagerScript singleton;

    [SyncVar]
    public string studycondition;

    [SerializeField]
    [SyncVar]
    private int numPlayers = 1;

    private void Awake()
    {
        singleton = this;
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (isServer) studycondition = MyNetworkManager.mySingleton.studyCondition;
        if (isServer) return;

        CMDMovingThePlayers(numPlayers, studycondition);        
    }
    
    /// <summary>
    /// Increments numPlayers and calls MovePlayer on each Client
    /// </summary>
    /// <param name="t"></param>
    [Command(requiresAuthority = false)]
    public void CMDMovingThePlayers(int t, string cond)
    {
        Debug.Log("Moving the Players");
        numPlayers++;
        RPCMovePlayer(t, cond);
    }

    /// <summary>
    /// Calls Move on playerRig, because it cant have the NetworkIdentity which Networkbehaviour demands.
    /// </summary>
    /// <param name="numPlayers"></param>
    [ClientRpc]
    public void RPCMovePlayer(int t, string cond)
    {
        playerRig.Move(t, cond);
    }
}
