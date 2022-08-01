using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OfflineSceneManager : MonoBehaviour
{
    public int phaseNumber;
    public TextMeshProUGUI text;
    public GameObject[] phaseOneObjects;
    public GameObject[] phaseTwoObjects;
    public GameObject[] phaseThreeObjects;
    public GameObject[][] phases;
    public MyNetworkManager networkManager;


    public void Start()
    {
        phases = new GameObject[][] { phaseOneObjects, phaseTwoObjects, phaseThreeObjects };

    }

    [ContextMenu("NextPhase")]
    public void SwitchToNextPhase()
    {
        if (phaseNumber == 3)
        {
            ChangeText();
            networkManager.StartClient();
            return;
        }
        if (phaseNumber != 0)
        {
           foreach (GameObject go in phases[phaseNumber-1])
           {
                        
             go.SetActive(false);        
           }

        }
        foreach (GameObject go in phases[phaseNumber])
        {
            go.SetActive(true);
        }
        phaseNumber++;
        ChangeText();
    }

    public void ChangeText()
    {
        if(phaseNumber == 1)
        {
            text.text = "Hier kannst du Deinen Avatar personalisieren.\nWähle deinen Look!\nWenn du fertig bist, drücke den roten Knopf.";
        }
        if(phaseNumber == 2)
        {
            text.text = "Im Folgenden müsst ihr gemeinsam drei Spiele spielen.\nDazu könnt ihr mit bestimmten Gegenständen interagieren. Diese sind in diesen Raum demonstriert.\nNimm sie doch mal in die Hand!\n Drücke den roten Knopf, wenn du bereit für den nächsten Schritt bist.";
        }
        if (phaseNumber == 3)
        {
            text.text = "Für jede Aufgabe habt ihr begrenzt Zeit. Der Wecker zeigt euch, wie lange ihr noch habt.\nIst die Zeit abgelaufen, klingelt der Wecker.\nUm zur nächsten Aufgabe zu gelangen, müsst ihr die Wecker abstellen und eure Daumen hochstrecken.\nMit dem Knopf vor dem Radio könnt ihr den Versuchsleiter jederzeit erreichen.\nBist Du bereit die Spiele zu starten drücke den roten Knopf";
        }
    }
}
