using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnerScript : NetworkBehaviour
{
    // Start is called before the first frame update

    public bool alreadySpawned = true;
    public GameObject head;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alreadySpawned)
        {
            SpawnHead();
            alreadySpawned = true;
        }
    }

    [Server]
    void SpawnHead()
    {
        GameObject gobj = Instantiate(head);
        NetworkServer.Spawn(gobj);

    }
}
