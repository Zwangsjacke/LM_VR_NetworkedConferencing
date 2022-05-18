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
    float currentTime;
    public int startingMinutes;
    public TextMeshProUGUI currentTimeText;
    private void Awake()
    {
        currentTime = startingMinutes * 60;
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
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }


    public void StartTimer()
    {
        timerActive = true;
    }

    public void EndTimer()
    {
        timerActive = false;
    }

    public void TimerFinished()
    {
        timerActive = false;
        Debug.Log("Timer finished");
        gameManager.endCondition = true;
    }
}
