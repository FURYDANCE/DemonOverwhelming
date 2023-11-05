using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 检测场景中是否存在相机与EventSystem,若不存在则生成
/// </summary>
public class CheckCameraAndEventSystemExist : MonoBehaviour
{
    public GameObject cameraPrefab;
    public GameObject eventSystemPrefab;
    private void Awake()
    {
        if (!GameObject.Find("Camera") || !GameObject.Find("EventSystem"))
        {
            Debug.Log("场景中没有Camera或EventSystem，自动生成对应的对象");
            if (!GameObject.Find("Camera"))
                Instantiate(cameraPrefab);
            if (!GameObject.Find("EventSystem"))
                Instantiate(eventSystemPrefab);
        }
    }
}
