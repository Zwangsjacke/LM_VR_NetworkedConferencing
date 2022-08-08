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
            text.text = "Hier kannst du Deinen Avatar personalisieren.\nW�hle deinen Look!\nWenn du fertig bist, dr�cke den roten Knopf.";
        }
        if(phaseNumber == 2)
        {
            text.text = "Im Folgenden m�sst ihr gemeinsam drei Spiele spielen.\nDazu k�nnt ihr mit bestimmten Gegenst�nden interagieren. Diese sind in diesen Raum demonstriert.\nNimm sie doch mal in die Hand!\n Dr�cke den roten Knopf, wenn du bereit f�r den n�chsten Schritt bist.";
        }
        if (phaseNumber == 3)
        {
            text.text = "F�r jede Aufgabe habt ihr begrenzt Zeit. Der Wecker zeigt euch, wie lange ihr noch habt.\nIst die Zeit abgelaufen, klingelt der Wecker.\nUm zur n�chsten Aufgabe zu gelangen, m�sst ihr die Wecker abstellen und eure Daumen hochstrecken.\nMit dem Knopf vor dem Radio k�nnt ihr den Versuchsleiter jederzeit erreichen.\nBist Du bereit die Spiele zu starten dr�cke den roten Knopf";
        }
    }
}
