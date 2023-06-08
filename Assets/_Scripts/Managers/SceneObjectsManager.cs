using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理场景中可能被其他类调用的对象的管理器，需要调用时调用其中的get方法
/// 思路是，难免会遇到需要其他类调用的对象，比如计算UI可能需要相机相关的变量，从这个单例中获取到所需的对象更好溯源？
/// </summary>
public class SceneObjectsManager : MonoBehaviour
{
    [Header("相机")]
    [SerializeField]
    private Transform camera;
    [Header("相机的左右边界")]
    [SerializeField]
    private Transform cameraBound_Left;
    [SerializeField]
    private Transform cameraBound_Right;


    public static SceneObjectsManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        if (camera == null)
            camera = GameObject.Find("CM vcam1").transform;
        if (cameraBound_Left == null)
            cameraBound_Left = GameObject.Find("LeftCameraBound").transform;
        if (cameraBound_Right == null)
            cameraBound_Right = GameObject.Find("RightCameraBound").transform;
    }
    /// <summary>
    /// 获取相机对象
    /// </summary>
    /// <returns></returns>
    public Transform GetCamera()
    {
        return camera;
    }
    /// <summary>
    /// 获取相机的左右边界
    /// </summary>
    /// <param name="L"></param>
    /// <param name="R"></param>
    public void GetCameraBound(out Transform L, out Transform R)
    {
        L = cameraBound_Left;
        R = cameraBound_Right;
    }
}
