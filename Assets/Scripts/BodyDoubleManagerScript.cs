using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class BodyDoubleManagerScript : MonoBehaviour
{
    // Hold Sources and Imitators of the players' movement
    [Header("Sources of player movement")]
    public GameObject playerHead;
    public GameObject playerLeftHand;
    public GameObject playerRightHand;

    [Header("Objects that will display player movement on the server")]
    public GameObject networkedHead;
    public GameObject networkedLeftHand;
    public GameObject networkedRightHand;

    private bool tracking = false;

    // Update is called once per frame
    void Update()
    {
        if (tracking)
        {
            TrackPlayerMovement();
        }
    }


    //Matches player movement to networked objects
    private void TrackPlayerMovement()
    {
        networkedHead.transform.position = playerHead.transform.position;
        networkedHead.transform.localRotation = playerHead.transform.localRotation;

        networkedLeftHand.transform.position = playerLeftHand.transform.position;
        networkedLeftHand.transform.localRotation = playerLeftHand.transform.localRotation;

        networkedRightHand.transform.position = playerRightHand.transform.position;
        networkedRightHand.transform.localRotation = playerRightHand.transform.localRotation;
    }


    // Spawns and matches bodyparts
    // Called by Networkmanager, when starting online scene
    public void MatchBodyPartsAndActivate()
    {

        MatchBodyParts();
        ActivateTracking();
        Debug.Log("Matched bodyparts and activated tracking");
    }

    // Enable tracking of original player movement onto networked player representation
    // Also Called by networkmanager, when starting online Scene
    public void ActivateTracking()
    {
        tracking = true;
        Debug.Log("Tracking activated");
    }

    // Fetches bodyparts for matching.
    // Necessary because objects arent in the scene at the start of it
    public void MatchBodyParts()
    {
        networkedHead = GameObject.FindGameObjectWithTag("networkedHead");
        networkedLeftHand = GameObject.FindGameObjectWithTag("networkedLeftHand");
        networkedRightHand = GameObject.FindGameObjectWithTag("networkedRightHand");

        playerHead = GameObject.FindGameObjectWithTag("playerHead");
        playerLeftHand = GameObject.FindGameObjectWithTag("playerLeftHand");
        playerRightHand = GameObject.FindGameObjectWithTag("playerRightHand");

        Debug.Log("Matched bodyparts");
    }

}
