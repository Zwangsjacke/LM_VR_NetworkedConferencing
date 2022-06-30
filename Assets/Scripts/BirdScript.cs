using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    [Header("InternalTimer")]
    public bool timerActive;
    public float countDown;
    public bool away;
    public float countDownTillLeave = 60;
    public float countDownTillArrive = 60;

    private void Start()
    {
        countDown = countDownTillLeave;
    }

    private void Update()
         {

        if (timerActive)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0 && !away)
            { 
                FlyAway();
                countDown = countDownTillArrive;
            }               
        }else if (countDown <=0 && away)
            {
                ComingBack();
                countDown = countDownTillArrive;
            }
    }
    public void FlyAway()
    {
        animator.SetBool("BirdCominBack", false);
        animator.SetBool("isFlying", true);
        away = true;

    }

    public void ComingBack()
    {
        animator.SetBool("isFlying", false);
        animator.SetBool("BirdCominBack", true);
        away = false;
    }


}
