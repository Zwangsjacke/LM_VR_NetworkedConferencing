using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameManagerScript gameManager;
    public bool timerActive = false;
    public float currentTime;
    public int startingMinutes;
    public int gameCount;
    public bool desertPhaseOneDone = false;

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
            if(currentTime <= 0)
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

    public void EndTimer()
    {

        timerActive = false;
    }

    /// <summary>
    /// Resets Timer and sets Endcondition true
    /// </summary>
    public void TimerFinished()
    {
        if(gameCount == 1 && !desertPhaseOneDone)
        {
            desertPhaseOneDone = true;
            currentTime= gameManager.desertSecondPhaseTimer * 60;
        }
        else
        {
        EndTimer();
        Debug.Log("Timer finished");
        gameManager.timerFinished = true;
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
}
