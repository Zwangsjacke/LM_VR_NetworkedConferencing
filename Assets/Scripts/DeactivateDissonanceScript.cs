using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dissonance;
public class DeactivateDissonanceScript : MonoBehaviour
{
    public GameObject dissonanceScriptHolder;

    private void Start()
    {
        if (MyNetworkManager.singelton.studyCondition == "VideoConference")
        {
            dissonanceScriptHolder.SetActive(false);
        }
    }
}
