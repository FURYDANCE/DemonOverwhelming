using UnityEngine;

/// <summary>
/// �ù��ش˽ű��Ķ����泯�����
/// </summary>
public class FaceToCamera : MonoBehaviour
{
    [Header("�Ƕȣ�������Ϊ45��")]
    public float angle;
    void Update()
    {
        transform.rotation = Quaternion.Euler(angle==0? -45:angle, transform.rotation.y, transform.rotation.z);
    }
}
