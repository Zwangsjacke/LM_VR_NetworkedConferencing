using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;


public class PhotoGamePad : MonoBehaviour
{
    public PhotoGame photoGame;

    public TextMeshProUGUI[] petTexts;

    public string[] petNames = new string[] { "Amigo", "Poopie", "Amigo", "Kitty" };

    public string[] solution;

    public string[] answers = new string[4];

    public TextMeshProUGUI endText;

    public GameObject[] buttons;

    public GameObject[] checks;

    public GameObject[] errors;

    public int correctAnswers = 0;

    public Sprite commitActive;

    public SpriteRenderer spriteRenderer;

    public float commitButtonDelaySec;

    public bool timerActive = true;

    public bool commitActiveBool;

    public void Awake()
    {

        photoGame = GameObject.FindGameObjectWithTag("gameManager").GetComponent<PhotoGame>();

        for(int i = 0; i < answers.Length; i++)
        {
            petTexts[i].text = petNames[i];
        }
    }

    private void Update()
    {
        if (timerActive)
        {
            commitButtonDelaySec -= Time.deltaTime;
            if (commitButtonDelaySec <= 0) SetCommitButtonActive();
        }
    }

    /// <summary>
    /// Changes the text of a specific petNameText field. Always chooses the next Name in PetNames and starts at Index 0 if it overshoots
    /// </summary>
    /// <param name="textNumber">Specifies which textfield to change</param>
    public void ChangeText(int textNumber)
    {
        string petText = petTexts[textNumber].text;
        int newId = Array.FindIndex(petNames, x => x.Contains(petText)) + 1;

        if(newId == 4) newId = 0;

        petTexts[textNumber].text = petNames[newId];
    }

    public void LogInAnswer()
    {
        if (!commitActiveBool) return;

        for(int i=0; i<answers.Length; i++)
        {
            answers[i] = petTexts[i].text;
        }
        DeactivateButtons();
        CheckAnswers();
        InformGameManager();
        SetEndText();
    }

    public void CheckAnswers()
    {
        for(int i = 0; i < 4; i++)
        {
            ToggleCheck(answers[i] == solution[i], i);
        }
    }

    public void ToggleCheck(bool isTrue, int checkId)
    {

        if (isTrue)
        {
            checks[checkId].SetActive(true);
            correctAnswers++;
        }
        else
        {
            errors[checkId].SetActive(true);
        }
    }

    /// <summary>
    /// Destroys buttons
    /// </summary>
    private void DeactivateButtons()
    {
        foreach(GameObject button in buttons)
        {
            button.SetActive(false);
        }
    }
    

    private void InformGameManager()
    {
        photoGame.SetCondition(); 
    }

    private void SetEndText()
    {
        
        endText.text = $"Du hast {correctAnswers} Pärchen korrekt zugewiesen!";
    }

    public void SetCommitButtonActive()
    {
        spriteRenderer.sprite = commitActive;
        timerActive = false;
        commitActiveBool = true;
    }




}
