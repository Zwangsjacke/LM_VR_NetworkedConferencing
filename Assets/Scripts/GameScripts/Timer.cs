using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameManagerScript gameManager;
    public bool timerActive = true;
    public float currentTime;
    public int startingMinutes;
    public int gameCount;
    public bool desertPhaseOneDone = false;
    public AudioSource audioSource;
    public Timer otherTimer;
    public GameObject ownArrow;
    public GameObject otherArrow;
    public bool arrowShown;

    public TextMeshProUGUI currentTimeText;

    /// <summary>
    /// Finds GameManager and sets starting Time
    /// </summary>
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0 )
            {
                TimerFinished();
            }
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString().PadLeft(2,'0') + ":" + time.Seconds.ToString().PadLeft(2, '0');
    }


    public void StartTimer()
    {
        GetStartingMinutes();
        currentTime = startingMinutes * 60;
        timerActive = true;
    }
    /// <summary>
    /// Doesnt play the AlarmSound if its the server
    /// </summary>
    public void EndTimer()
    {

        timerActive = false;
        if (gameManager.gameCounter != 0)
        {
            gameManager.GameEnded();
        }
        if (gameManager.networkGameManager.isServer) return;
        PlayAlarm();
    }

    /// <summary>
    /// Resets Timer and sets Endcondition true
    /// </summary>
    public void TimerFinished()
    {
        if(gameCount == 1 && !desertPhaseOneDone)
        {
            desertPhaseOneDone = true;
            currentTime = gameManager.desertSecondPhaseTimer * 60;
            //GameObject header = GameObject.FindGameObjectWithTag("Ranking Header");
            //header.SetActive(true);
            foreach (TextMeshProUGUI txt in gameManager.disyplays)
            {
                txt.text = "Welcher Gegenstand kann euch am meisten helfen?\n Bringt die Gegenstände nun in Rangreihenfolge und pinnt die Fotos an die richtige Stelle!";
            }


        }
        else
        {
        EndTimer();
        Debug.Log("Timer finished");
        
        }
    }

    /// <summary>
    /// Sets starting minutes for the specific game
    /// -1 to counter the increment in GamesManager
    /// </summary>
    private void GetStartingMinutes()
    {

        gameCount = gameManager.gameCounter;
        startingMinutes = gameManager.gameTimers[gameCount];
       
    }

    private void PlayAlarm()
    {
        if(!arrowShown)
        {
            ownArrow.SetActive(true);
            arrowShown = true;
        }
        audioSource.Play();
    }

    public void TurnAlarmOff()
    {        
        ownArrow.SetActive(false);
        otherArrow.SetActive(false);
        if (timerActive) return;
        otherTimer.audioSource.Stop();
        audioSource.Stop();
        gameManager.timerFinished = true;
    }

    


}
