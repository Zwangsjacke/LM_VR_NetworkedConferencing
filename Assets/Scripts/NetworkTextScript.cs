using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkTextScript : MonoBehaviour
{
    public TextMesh textMesh;
    public MyNetworkManager networkManager;
    public string text = "192.168.11.121";
    // Start is called before the first frame update


    public void AddNumber(string newNumber)
    {
        if(text.Length < 15)
        {
            text += newNumber;
            textMesh.text = text;
        }
    }

    public void Delete()
    {
        text = text.Remove(text.Length-1);
        textMesh.text = text;
    }

    public void SendTextToManagerAndConnect()
    {
        networkManager.SendMessage("SetIp", text);
        networkManager.SendMessage("StartClient"); 
    }
}
