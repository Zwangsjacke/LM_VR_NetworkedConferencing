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


    // Start is called before the first frame update

    public void Awake()
    {

    }
    // Update is called once per frame


    public override void OnStartClient()
    {
        base.OnStartClient();

        MovingThePlayers(numPlayers);
        Debug.Log("Test");
    }

    [Command(requiresAuthority = false)]
    public void MovingThePlayers(int t)
    {
        MovePlayer(t);
    }

    [ClientRpc]
    public void MovePlayer(int x)
    {
        numPlayers++;
        playerRig.Move(numPlayers);
    }
}
