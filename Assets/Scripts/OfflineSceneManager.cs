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
    public ToBlackFader fader;
    public AudioSource switchAudio;


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
            StartCoroutine(SwitchToOnline());
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
        PlaySound();
        ChangeText();

    }

    private IEnumerator SwitchToOnline()
    {
        fader.Fade(true);
        yield return new WaitForSeconds(3);
        networkManager.StartClient();
    }

    public void PlaySound()
    {
        switchAudio.Play();
    }


    public void ChangeText()
    {
        if(phaseNumber == 1)
        {
            text.text = "Hier kannst du Deinen Avatar personalisieren.\n" +
                "Kombiniere Farbe, Kleidung und K�rper und w�hle deinen Look!\n" +
                "Rechts kannst Du die Optionen von Kleidung und K�rper w�hlen. Links kannst Du die Farbe der gew�hlten Option anpassen.\n"+
                "Wenn du fertig bist, dr�cke den roten Knopf.";
        }
        if(phaseNumber == 2)
        {
            text.text = "Ihr werdet gemeinsam drei Spiele spielen.\n" +
                "Dazu k�nnt ihr mit bestimmten Gegenst�nden interagieren.\n" +
                "Nimm sie doch mal in die Hand! Versuch die Gegenst�nde herum zu drehen und an verschiedenen Stellen zu greifen.\n" +
                "Dr�cke den roten Knopf, wenn du bereit f�r den n�chsten Schritt bist.";
        }
        if (phaseNumber == 3)
        {
            text.text = "F�r jede Aufgabe habt ihr begrenzt Zeit. Ist die Zeit abgelaufen, klingelt der Wecker.\n" +
                "Um zur n�chsten Aufgabe zu gelangen, muss jeder von euch den Wecker abstellen und einen Daumen hochstrecken.\n" +
                "Mit dem Knopf vor dem Radio k�nnt ihr den Versuchsleiter jederzeit erreichen.\nBist Du bereit den n�chsten Raum zu betreten und die Spiele zu starten, dr�cke den roten Knopf";
        }
    }
}
