using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseGame : MonoBehaviour
{
    [Tooltip("0 = Alarm\n1 = Clipboard\n2 = Pinboard And Pins\n3 = Owner Profiles\n4 = Pet Profiles\n5 = GamePad")]
    public int[] prefabIds;

    public GameManagerScript gameManager;
    public NetworkGameManager networkGameManager;

    [Header("SpawnLocations")]
    public Transform[] SpawnLocationsPlayerOne;
    public Transform[] SpawnLocationsPlayerTwo;

    [Header("Display")]
    //public TextMeshProUGUI[] displays;
    public string gameText;

    public Timer timer;

    /// <summary>
    /// Destorys all previous game objects, changes the dispayed text and spawns new ones specific to the game
    /// </summary>
    public virtual void StartGame()
    {
        ClearGamePrefabs();
        //ChangeGameText();
        SpawnObjects();
        
    }

    /// <summary>
    /// Spawns all Objects relevant for the Game. Stored in Arrays
    /// </summary>
    public virtual void SpawnObjects()
    {

        for (int i = 0; i < prefabIds.Length; i++)
        {
            networkGameManager.SpawnObjects(prefabIds[i], SpawnLocationsPlayerOne[i], SpawnLocationsPlayerTwo[i]);
        }

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
    //public void ChangeGameText()
    //{
    //    foreach (TextMeshProUGUI txt in displays)
    //    {
    //        txt.text = gameText;
    //    }

    //}

    public void StartTimer()
    {
        if(timer == null)
        {
           timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>();
        }

        timer.StartTimer();
    }

}
