using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ������󣨲���Ϊ�����壬����������棩
/// </summary>
public class FollowObject : MonoBehaviour
{
    public Transform followTarget;
    public Vector3 followOffset;
    // Update is called once per frame
    void Update()
    {
        if(followTarget)
        transform.position = followTarget.position + followOffset;
    }
}
