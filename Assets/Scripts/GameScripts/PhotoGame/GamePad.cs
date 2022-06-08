using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePad : MonoBehaviour
{
    public GameManagerScript gameManager;
    public string answer = "";

    public TextMeshProUGUI firstNumberText;
    public TextMeshProUGUI secondNumberText;
    public TextMeshProUGUI thirdNumberText;
    public TextMeshProUGUI fourthNumberText;

    public string correctAnswer ="1111";
    public int firstNumber = 1;
    public int secondNumber = 1;
    public int thirdNumber = 1;
    public int fourthNumber = 1;

    public GameObject[] gosCorrect;
    public GameObject[] gosFalse;


    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManagerScript>();

        gosCorrect = GameObject.FindGameObjectsWithTag("correct");
        gosFalse = GameObject.FindGameObjectsWithTag("false");
        foreach(GameObject go in gosCorrect)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in gosFalse)
        {
            go.SetActive(false);
        }
    }
    public void IsCorrect()
    {

        if (answer[0] == correctAnswer[0])
        {
            gosCorrect[0].SetActive(true);
        }
        else
        {
            gosFalse[0].SetActive(true);
        }

        if (answer[1] == correctAnswer[1])
        {
            gosCorrect[1].SetActive(true);
        }
        else
        {
            gosFalse[1].SetActive(true);
        }

        if (answer[2] == correctAnswer[2])
        {
            gosCorrect[2].SetActive(true);
        }
        else
        {
            gosFalse[2].SetActive(true);
        }

        if (answer[3] == correctAnswer[3])
        {
            gosCorrect[3].SetActive(true);
        }
        else
        {
            gosFalse[3].SetActive(true);
        }

        gameManager.endCondition = true;

    }

    public void ChangeText(int letter)
    {
        switch (letter)
        {
            case 1:
                if(firstNumber < 3)
                {
                    firstNumber++;
                }
                else
                {
                    firstNumber = 1;
                }

                firstNumberText.text = firstNumber.ToString();
                break;
            case 2:
                if (secondNumber < 3)
                {
                    secondNumber++;
                }
                else
                {
                    secondNumber = 1;
                }

                secondNumberText.text = secondNumber.ToString();
                break;
            case 3:
                if (thirdNumber < 3)
                {
                    thirdNumber++;
                }
                else
                {
                    thirdNumber = 1;
                }

                thirdNumberText.text = thirdNumber.ToString();
                break;
            case 4:
                if (fourthNumber < 3)
                {
                    fourthNumber++;
                }
                else
                {
                    fourthNumber = 1;
                }

                fourthNumberText.text = fourthNumber.ToString();
                break;
        }
    }


    /// <summary>
    /// empties answer string and refills it with displayed numbers
    /// </summary>
    public void LogInAnswer()
    {
        answer = "";
        answer += firstNumber.ToString();
        answer += secondNumber.ToString();
        answer += thirdNumber.ToString();
        answer += fourthNumber.ToString();


        IsCorrect();
    }
}
