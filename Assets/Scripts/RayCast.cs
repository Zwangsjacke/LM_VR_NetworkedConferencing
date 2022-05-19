using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Mirror;

public class RayCast : NetworkBehaviour
{
    public GameObject[] raycastSources;
    public GameObject raySourceOne;
    public GameObject raySourceTwo;
    public bool startedTracking;
    public bool foundTwoSources;
    public float watchTimePlayerOne;
    public float watchTimePlayerTwo;
    RaycastHit hit;
    RaycastHit hit2;
    string watchedObjectOne;
    string watchedObjectTwo;
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

        if (!foundTwoSources)
        {
            BeginTracking();
        }



        if (startedTracking)
        {
            if (Physics.Raycast(raySourceOne.transform.position, raySourceOne.transform.forward, out hit, 100f))
            {
                
                if (hit.transform.CompareTag("plant"))
                {
                    watchedObjectOne = hit.transform.tag;
                    watchTimePlayerOne += Time.deltaTime;

                }
                else if (watchTimePlayerOne != 0)
                {
                    WriteFile(watchedObjectOne, watchTimePlayerOne, 1);
                    watchTimePlayerOne = 0;
                }

            }
            
            if (Physics.Raycast(raySourceTwo.transform.position, raySourceTwo.transform.forward, out hit2, 100f))
            {
                if (hit2.transform.CompareTag("plant"))
                {
                    watchedObjectTwo = hit2.transform.tag;
                    watchTimePlayerTwo += Time.deltaTime;

                }
                else if (watchTimePlayerTwo != 0)
                {
                    WriteFile(watchedObjectTwo, watchTimePlayerTwo, 2);
                    watchTimePlayerTwo = 0;
                }

            }
            

        }




    }


    public void BeginTracking()
    {

        raycastSources = GameObject.FindGameObjectsWithTag("rayCastSource");
        if(raycastSources.Length == 2)
        {
            raySourceOne = raycastSources[0];
            raySourceTwo = raycastSources[1];
            startedTracking = true;
            foundTwoSources = true;
            Debug.Log("Gaze Tracking Started");
        }


    }
    void BeginFile()
    {
        TextWriter tw = new StreamWriter(_fileName, false);
        tw.WriteLine("Player, Incident Number, Duration, Time");
        tw.Close();
    }

    void WriteFile(string objectName, float duration, int playerId)
    {
        TextWriter tw = new StreamWriter(_fileName, true);

        string time = System.DateTime.Now.ToString();

        TimeSpan timePassed = TimeSpan.FromSeconds(duration);
        string durationText = timePassed.Seconds.ToString() + ":" + timePassed.Milliseconds.ToString();
        

        tw.WriteLine($"{playerId},{objectName}, {durationText}, {time}", "/b");

        tw.Close();

        Debug.Log("Wrote File");
    }


}
