using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVWriter : MonoBehaviour
{
    string _fileName = "";
    // Start is called before the first frame update
    void Start()
    {
        _fileName = Application.dataPath + "/TesterFile.csv";
        WriteCSV();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Wenn StreamWriter false, dann wird Datei überschrieben, bei true wird sie ergänzt
    /// </summary>
    public void WriteCSV()
    {
        TextWriter tw = new StreamWriter(_fileName, false);
        tw.WriteLine("Incident Number, Time, Duration");
        tw.Close();

        tw = new StreamWriter(_fileName, true);

        tw.WriteLine("Test1, Test2, Test3", "/b");
        tw.WriteLine("Test1, Test2, Test3", "/b");
        tw.Close();

        Debug.Log("Wrote File");
    }
}
