using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.IO;

public class DataTracker : NetworkBehaviour
{
    public GameObject rayStart;
    public float watchTime;
    string watchedObject;
    string _fileName = "";

    // Start is called before the first frame update
    void Start()
    {
        _fileName = Application.dataPath + "/TesterFile.csv";
        BeginFile();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(rayStart.transform.position, rayStart.transform.forward, out hit, 100f))
        {
            if (hit.transform.CompareTag("plant"))
            {
                watchedObject = hit.transform.tag;
                watchTime += Time.deltaTime;

            }
            else if (watchTime != 0)
            {
                WriteFile(watchedObject, watchTime);
                watchTime = 0;
            }

        }



    }

    void BeginFile()
    {
        TextWriter tw = new StreamWriter(_fileName, false);
        tw.WriteLine("Incident Number, Duration, Time");
        tw.Close();
    }

    void WriteFile(string objectName, float duration)
    {
        TextWriter tw = new StreamWriter(_fileName, true);

        string time = System.DateTime.Now.ToString();

        TimeSpan timePassed = TimeSpan.FromSeconds(duration);
        string durationText = timePassed.Seconds.ToString() + ":" + timePassed.Milliseconds.ToString();


        tw.WriteLine($"{objectName}, {durationText}, {time}", "/b");

        tw.Close();

        Debug.Log("Wrote File");
    }


}
