using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PhoneScript : MonoBehaviour
{
    public Material screenMaterial;
    public Texture phoneOff;
    public Texture phoneOn;
    public MeshRenderer meshRenderer;
    public AudioSource audioSourceNotification;
    public AudioSource audioSourceVibrate;
    public GameObject NotificationObject;
    public bool isNotified;
    public TextMeshProUGUI clock;
    public bool timerActive;
    public float countdownSec = 60;
    public NetworkGameManager networkedGameManager;

    private void Awake()
    {
        TurnPhoneOff();
        StartTimer();
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void Update()
    {
        if (timerActive)
        {
            countdownSec -= Time.deltaTime;
            if(countdownSec <= 0)
            {
                timerActive = false;
                GetNotification();
            }

        }
    }
    public void Dismiss()
    {
        networkedGameManager.CMDTurnPhonesOff();
    }

    public void GetNotification()
    {
        Vibrate();
        TurnPhoneOn();
        PlayNotificationSound();
        ShowNotification();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (!isNotified)
            {
                TurnPhoneOn();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (!isNotified)
            {
                TurnPhoneOff();
            }
        }
    }

    public void ShowNotification()
    {
        NotificationObject.SetActive(true);
    }

    public void TurnPhoneOn()
    {        
        screenMaterial.mainTexture = phoneOn;
        SetClock();
        
    }

    public void TurnPhoneOff()
    {
        NotificationObject.SetActive(false);
        StopVibrating();
        screenMaterial.mainTexture = phoneOff;
        clock.text = null;
    }

    public void SetClock()
    {
        DateTime time = DateTime.Now;
        string hour = time.Hour.ToString().PadLeft(2,'0');
        string minute = time.Minute.ToString().PadLeft(2,'0');
        
        

        clock.text = string.Format("{01:00}:{00:00}", minute, hour);

    }

    public void Vibrate()
    {
        
        audioSourceVibrate.Play();
    }

    public void PlayNotificationSound()
    {
        
        audioSourceNotification.Play();
    }

    public void StopVibrating()
    {
        audioSourceVibrate.Stop();
    }

}
