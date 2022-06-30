using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    public void FlyAway()
    {
        animator.SetBool("ComingBack", false);
        audioSource.Play();
        animator.SetBool("isFlying", true);
    }

    public void ComingBack()
    {
        audioSource.Play();
        animator.SetBool("isFyling", false);
        animator.SetBool("ComingBack", true);
    }


}
