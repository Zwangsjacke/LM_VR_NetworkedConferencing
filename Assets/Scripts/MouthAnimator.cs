using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using NAudio.Wave;
using Dissonance;
using Dissonance.Audio.Capture;

public class MouthAnimator : NetworkBehaviour, IMicrophoneSubscriber
{
    public SkinnedMeshRenderer head;
    public float volume;
    public float value;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            FindObjectOfType<DissonanceComms>().SubscribeToRecordedAudio(this);
        }

    }

    public void ReceiveMicrophoneData(ArraySegment<float> buffer, WaveFormat format)
    {
        if (isLocalPlayer)
        {
            float[] data = buffer.Array;
            float average = 0;
            for (int i = 0; i < data.Length; i++)
            {
                average += Math.Abs(data[i]);
            }
            average /= data.Length;
            volume = average;
        }
    }

    float lastValue = 0;
    void FixedUpdate()
    {
        if (head && isLocalPlayer)
        {
            int pos = (volume > 0.03F) ? 100 : 0;
            value = Mathf.MoveTowards(value, pos, Time.deltaTime * 1000F);
            if (value != lastValue)
            {
                head.SetBlendShapeWeight(0, value);
                CmdSetMouth(value);
                lastValue = value;
            }
        }
    }

    public void Reset()
    {
        
    }

    [Command]
    private void CmdSetMouth(float value)
    {
        if (head)
            head.SetBlendShapeWeight(0, value);
        RpcSetMouth(value);
    }

    [ClientRpc]
    private void RpcSetMouth(float value)
    {
        if (head)
            head.SetBlendShapeWeight(0, value);
    }

}
