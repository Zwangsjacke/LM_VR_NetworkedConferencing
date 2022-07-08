using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurvedTextScript : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void Awake()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }
    public void ChangeText(string newText)
    {
        text.text = newText;
    }
}
