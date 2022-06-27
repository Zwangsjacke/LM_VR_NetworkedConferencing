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
    public int desertSecondPhaseTimer;
    public int gameCounter = 0;
    public bool endCondition = false;
    public bool alreadyThumbs = false;
    public bool timerFinished;

    [Header("Display Text")]
    public string studyEndHeader;
    public string studyEndBody;
    public Timer TimerOne;
    public Timer TimerTwo;

    [Header("AudioSources")]
    public AudioSource changeGameSound;
    public AudioSource thumbsUpSound;

    

    /// <summary>
    /// Increments the gameCounter an starts the respective Game or ends the study. See BaseGame[] games order for the game order.
    /// Resets endConditions
    /// </summary>
    public void StartNextGame()
    {
        endCondition = false;
        alreadyThumbs = false;
        timerFinished = false;
        changeGameSound.Play();
        if (gameCounter <= 2)
        {
            
            games[gameCounter].StartGame();
            TimerOne.StartTimer();
            TimerTwo.StartTimer();

        }

        else
        {
            EndVRStudy();
        }
        gameCounter++;
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
            thumbsUpSound.Play();
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
