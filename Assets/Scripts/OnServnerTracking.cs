using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Mirror;

public class OnServnerTracking : NetworkBehaviour
{
    public List<GameObject> raySources;
    public List<float> watchTimes;
    public List<string> watchedObjects;
    public string[] tags = new string[] { "Window", "Picture", "Bird", "Fish", "Door", "InstructionScreen", "plantFarn", "plantAloe", "Lamp", "Player", "Roof" };
    public string fileName;


    void Start()
    {
        fileName = Application.dataPath + "/Durchlauf_"+ MyNetworkManager.mySingleton.experimentNumber + "_Bedingung_" + MyNetworkManager.mySingleton.studyCondition + ".csv";
        BeginFile();
    }
    private void Update()
    {
        if (!isServer) return;
        for (int i = 0; i < raySources.Count; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(raySources[i].transform.position, raySources[i].transform.forward, out hit, 100f))
        {

            if (TagExistsinArray(hit.transform.tag))
            {
                watchedObjects[i] = hit.transform.tag;
                watchTimes[i] += Time.deltaTime;
            }
            else if (watchTimes[i] != 0)
            {
                WriteFile(i, watchedObjects[i], watchTimes[i]);
                watchTimes[i] = 0;
            }


        }

        }

    }
    void BeginFile()
    {
        TextWriter tw = new StreamWriter(fileName, false);
        tw.WriteLine("Player, Object, Duration, Time");
        tw.Close();
        Debug.Log("Started File");
    }

    void WriteFile(int playerNumber, string objectName, float duration)
    {
        TextWriter tw = new StreamWriter(fileName, true);

        string time = System.DateTime.Now.ToString();

        TimeSpan timePassed = TimeSpan.FromSeconds(duration);
        string durationText = timePassed.Seconds.ToString() + ":" + timePassed.Milliseconds.ToString();


        tw.WriteLine($"Player {playerNumber}, {objectName}, {durationText}, {time}", "/b");

        tw.Close();

        Debug.Log("Wrote File");
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
    public void AddPlayer(GameObject player)
    {
        raySources.Add(player.GetComponent<NetworkPlayer>().head);
        watchTimes.Add(0);
        watchedObjects.Add("");
    }

}