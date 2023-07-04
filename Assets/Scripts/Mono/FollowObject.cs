using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 跟随对象（不作为子物体，单纯坐标跟随）
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
