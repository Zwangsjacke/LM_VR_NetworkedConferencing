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
            text.text = "Hier kannst du einen Avatar personalisieren. Wenn du fertig bist dr�cke den roten Knopf.\nW�hle deinen Look!";
        }
        if(phaseNumber == 2)
        {
            text.text = "Im Folgenden m�sst ihr gemeinsam drei Spiele spielen.\nDazu k�nnt ihr mit bestimmten Gegenst�nden interagieren. Diese sind in diesen Raum demonstriert. Nimm sie doch mal in die Hand!\n Dr�cke den roten Knopf, wenn du bereit f�r den n�chsten Schritt bist.";
        }
        if (phaseNumber == 3)
        {
            text.text = "F�r jede Aufgabe habt ihr begrenzt Zeit. Der Wecker zeigt euch, wie lange ihr noch habt. Ist die Zeit abgelaufen, klingelt der Wecker.\nUm zur n�chsten Aufgabe zu gelangen, m�sst ihr die Wecker abstellen und eure Daumen hochstrecken.\nMit dem Knopf vor dem Radio k�nnt ihr den Versuchsleiter jederzeit erreichen.\nBist Du bereit die Studie zu starten dr�cke den roten Knopf";
        }
    }
}
