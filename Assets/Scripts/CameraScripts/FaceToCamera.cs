using UnityEngine;

/// <summary>
/// �ù��ش˽ű��Ķ����泯�����
/// </summary>
public class FaceToCamera : MonoBehaviour
{


    void Update()
    {
        transform.rotation = Quaternion.Euler(-45, transform.rotation.y, transform.rotation.z);
    }
}
