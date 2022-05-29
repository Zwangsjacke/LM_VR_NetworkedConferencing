using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGame : MonoBehaviour
{
    [Tooltip("Hier kommen noch welche ID zu welchem Spiel gehört")]
    public int prefabId;

    public GameManagerScript gameManager;
    public NetworkGameManager networkGameManager;

    [Header("SpawnLocations")]
    public Transform spawnLocationOne;
    public Transform spawnLocationTwo;
    public string header;
    public string body;

    /// <summary>
    /// Destorys all previous game objects, changes the dispayed text and spawns new ones specific to the game
    /// </summary>
    public virtual void StartGame()
    {
        ClearGamePrefabs();
        ChangeGameText();

        networkGameManager.SpawnObjects(prefabId, spawnLocationOne, spawnLocationTwo);
    }

    /// <summary>
    /// Sets the endcondition to true, so that the next game can be started
    /// </summary>
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

    /// <summary>
    /// Changes disyplayed text to game specific text
    /// </summary>
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
