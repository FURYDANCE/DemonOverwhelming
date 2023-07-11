using UnityEngine;

/// <summary>
/// 让挂载此脚本的对象面朝摄像机
/// </summary>
public class FaceToCamera : MonoBehaviour
{


    void Update()
    {
        transform.rotation = Quaternion.Euler(-45, transform.rotation.y, transform.rotation.z);
    }
}
