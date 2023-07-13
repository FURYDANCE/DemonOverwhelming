using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Vector3 target;
    public float time;
    bool moving;
    Vector3 s;
    public void StartMove(Vector3 target,float time)
    {
        this.target = target;
        this.time = time;
        moving = true;
    }
    void Update()
    {
        if (moving)
            transform.position = Vector3.SmoothDamp(transform.position, target, ref s, time);
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            moving = false;
        }
    }
    public bool GetMoving()
    {
        return !moving;
    }
}
