using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 让挂载此脚本的对象的所有子对象面朝摄像机
/// </summary>
public class FaceToCamera : MonoBehaviour
{


    public Transform[] childs;
    // Start is called before the first frame update
    void Start()
    {
        childs = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform t in childs)
        {
            t.rotation = Camera.main.transform.rotation;
        }
    }
}
