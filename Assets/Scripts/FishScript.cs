using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public Transform goalPos;
    public float speed;
    public float rotationSpeed;

    private void Update()
    {
        Turn();
        Move();
    }


    private void Move()
    {
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    private void Turn()
    {
        Vector3 relativPos = goalPos.localPosition - transform.localPosition;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.LookRotation(relativPos), Time.deltaTime * rotationSpeed);
    }
}
