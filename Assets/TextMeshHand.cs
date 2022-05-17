using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshHand : MonoBehaviour
{
    public TextMeshProUGUI zex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeText()
    {
        zex.text = "Pose :)";
    }
    public void changeback()
    {
        zex.text = "No pose :(";
    }
}
