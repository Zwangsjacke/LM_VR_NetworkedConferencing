using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public NetworkGameManager networkGameManager;

    [Tooltip("Order relevant! \nShould be: Interview, Desert Survival, Photos")]
    public BaseGame[] games;
    [Tooltip("Timer in Minutes\nMatch the order above!")]
    public int[] gameTimers;
    public int gameCounter = 0;
    public bool endCondition = false;
    public bool alreadyThumbs = false;
    public bool timerFinished;
    [Header("Display Text")]
    public string studyEndHeader;
    public string studyEndBody;



    /// <summary>
    /// Increments the gameCounter an starts the respective Game or ends the study. See BaseGame[] games order for the game order.
    /// Resets endConditions
    /// </summary>
    public void StartNextGame()
    {
        if (gameCounter <= 3)
        {
        endCondition = false;
        alreadyThumbs = false;
        timerFinished = false;

        games[gameCounter].StartGame();
        gameCounter++;
        }

        else
        {
            EndVRStudy();
        }
    }

    /// <summary>
    /// If endcondition of the current game is met notify the network gamemanager
    /// Also checks if this already happend and resets every new game
    /// </summary>
    public void ThumbsUp()
    {
        if (endCondition && !alreadyThumbs && timerFinished)
        {
            alreadyThumbs = true;
            networkGameManager.CMDThumbsUp();
        }
    }

    /// <summary>
    /// Destorys all GameObjects and displays Study End Message
    /// </summary>
    public void EndVRStudy()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("GamePrefab"))
        {
            Destroy(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("header"))
        {
            go.SendMessage("ChangeText", studyEndHeader);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("body"))
        {
            go.SendMessage("ChangeText", studyEndBody);
        }
    }
}
