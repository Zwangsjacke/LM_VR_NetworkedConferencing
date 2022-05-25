using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkTextScript : MonoBehaviour
{
    public TextMesh textMesh;
    public MyNetworkManager networkManager;
    public string text = "192.168.11.118";


    /// <summary>
    /// Adds a number to the displayed string. Could also be any char.
    /// Used by KeyPad.
    /// </summary>
    /// <param name="newNumber">must be a string</param>
    public void AddNumber(string newNumber)
    {
        if(text.Length < 15)
        {
            text += newNumber;
            textMesh.text = text;
        }
    }

    /// <summary>
    /// Deletes the last char in the displayed string
    /// Used by deleteButton
    /// </summary>
    public void Delete()
    {
        text = text.Remove(text.Length-1);
        textMesh.text = text;
    }

    /// <summary>
    /// Sets the Ip of the NetworkManager and starts the Client.
    /// Used by EnterButton
    /// </summary>
    public void SendTextToManagerAndConnect()
    {
        networkManager.SendMessage("SetIp", text);
        networkManager.SendMessage("StartClient"); 
    }
}
