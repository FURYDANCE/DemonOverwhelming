using UnityEngine;

/// <summary>
/// 让挂载此脚本的对象面朝摄像机
/// </summary>
public class FaceToCamera : MonoBehaviour
{
    [Header("角度，不填则为45度")]
    public float angle;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(angle==0? 45:angle, transform.rotation.y, transform.rotation.z);
    }
    
}
