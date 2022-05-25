using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame : MonoBehaviour
{
    public int prefabId;
    public GameManagerScript gameManager;
    public NetworkGameManager networkGameManager;

    [Header("SpawnLocations")]
    public Transform spawnLocationOne;
    public Transform spawnLocationTwo;
    public string header;
    public string body;

    public virtual void StartGame()
    {
        ClearGamePrefabs();
        ChangeGameText();

        networkGameManager.SpawnObjects(prefabId, spawnLocationOne, spawnLocationTwo);
    }

    public virtual void SetCondition()
    {
        gameManager.endCondition = true;
    }

    /// <summary>
    /// Destroys GO with GamePrefab Tag
    /// </summary>
    public void ClearGamePrefabs()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("GamePrefab");

        foreach (GameObject go in gos)
        {
            Destroy(go);
        }
    }

    public void ChangeGameText()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("header"))
        {
            go.SendMessage("ChangeText", header);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("body"))
        {
            go.SendMessage("ChangeText", body);
        }

    }

}
