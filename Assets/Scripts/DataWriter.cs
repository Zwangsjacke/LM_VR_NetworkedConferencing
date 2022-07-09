using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.IO;
using System;

public class DataWriter : NetworkBehaviour
{
    public GameObject rayStart;
    public float watchTime;
    string watchedObject;
    public string[] tags;

    public string fileNameOne;
    public string fileNameTwo;

    private void Start()
    {
        if (isServer) return;
        CMDStartFile();
    }

    void Update()
    {
        if (isServer) return;
        RaycastHit hit;

        if (Physics.Raycast(rayStart.transform.position, rayStart.transform.forward, out hit, 100f))
        {

            if (TagExistsinArray(hit.transform.tag))
            {
                watchedObject = hit.transform.tag;
                watchTime += Time.deltaTime;
            }
            else if (watchTime != 0)
            {
                MakeData(watchedObject, watchTime);()
                watchTime = 0;
            }

        }

    }

    void MakeData(string objectName, float duration)
    {
        if (isServer) return;
        string time = System.DateTime.Now.ToString();

        TimeSpan timePassed = TimeSpan.FromSeconds(duration);
        string durationText = timePassed.Seconds.ToString() + ":" + timePassed.Milliseconds.ToString();

        string data = $"{objectName}, {durationText}, {time}";

        CMDWrite(data);
    }

    public void StartFile(int playerNumber)
    {
        if (!isServer) return;

        if(playerNumber == 1)
        {
            fileNameOne = Application.dataPath + $"/{MyNetworkManager.singelton.studyCondition.ToUpper()}{playerNumber}.csv";
            TextWriter tw = new StreamWriter(fileNameOne, false);
            tw.WriteLine("Incident Number, Duration, Time");
            tw.Close();
            Debug.Log("Startet File of Player One");
        }
        else
        {
            fileNameTwo = Application.dataPath + $"/{MyNetworkManager.singelton.studyCondition.ToUpper()}{playerNumber}.csv";
            TextWriter tw = new StreamWriter(fileNameTwo, false);
            tw.WriteLine("Incident Number, Duration, Time");
            tw.Close();
            Debug.Log("Startet File of Player Two");
        }

    }

    public void WriteFile(int playerNumber, string data)
    {
        if (!isServer) return;

        if (playerNumber == 1)
        {
            TextWriter tw = new StreamWriter(fileNameOne, true);
            tw.WriteLine(data, "/b");
            tw.Close();
        }
        else
        {
            TextWriter tw = new StreamWriter(fileNameTwo, true);
            tw.WriteLine(data, "/b");
            tw.Close();
        }
    }


    public bool TagExistsinArray(string tag)
    {
        if (Array.Exists(tags, element => element == tag))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



[Command]
    public void CMDStartFile()
    {
        StartFile(MyNetworkManager.singelton.playerNumber);
    }

    [Command]
    public void CMDWrite(string data)
    {
        WriteFile(MyNetworkManager.singelton.playerNumber, data);
    }
}
