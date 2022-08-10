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
                "Kombiniere Farbe, Kleidung und Körper und wähle deinen Look!\n" +
                "Rechts kannst Du die Optionen von Kleidung und Körpers wählen. Links kannst Du die Farbe der gewählten Option anpassen.\n"+
                "Wenn du fertig bist, drücke den roten Knopf.";
        }
        if(phaseNumber == 2)
        {
            text.text = "Ihr werdet gemeinsam drei Spiele spielen.\n" +
                "Dazu könnt ihr mit bestimmten Gegenständen interagieren.\n" +
                "Nimm sie doch mal in die Hand! Versuch die Gegenstände herum zu drehen und an verschiedenen Stellen zu greifen.\n" +
                "Drücke den roten Knopf, wenn du bereit für den nächsten Schritt bist.";
        }
        if (phaseNumber == 3)
        {
            text.text = "Für jede Aufgabe habt ihr begrenzt Zeit. Ist die Zeit abgelaufen, klingelt der Wecker.\n" +
                "Um zur nächsten Aufgabe zu gelangen, muss jeder von euch den Wecker abstellen und einen Daumen hochstrecken.\n" +
                "Mit dem Knopf vor dem Radio könnt ihr den Versuchsleiter jederzeit erreichen.\nBist Du bereit den nächsten Raum zu betreten und die Spiele zu starten, drücke den roten Knopf";
        }
    }
}
