using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPosScript : MonoBehaviour
{
    [Header("Constraints")]
    public Transform top;
    public Transform bottom;
    public Transform right;
    public Transform left;
    public Transform front;
    public Transform back;

    private void Start()
    {
        MovePosition();
    }



    public void MovePosition()
    {
        Vector3 pos = RandomPos();
        transform.localPosition = pos;
        Invoke("MovePosition", RandomTime());
    }

    private float RandomTime()
    {
        float time = Random.Range(2, 5);
        return time;
    }

    private Vector3 RandomPos()
    {
        Vector3 position = new Vector3(Random.Range(front.transform.localPosition.x, back.transform.localPosition.x), Random.Range(top.transform.localPosition.y, bottom.transform.localPosition.y), Random.Range(left.transform.localPosition.z, right.transform.localPosition.z));
        
        return position;
    }
}
