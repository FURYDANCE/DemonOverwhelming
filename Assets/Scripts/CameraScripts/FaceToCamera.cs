using UnityEngine;

/// <summary>
/// �ù��ش˽ű��Ķ����泯�����
/// </summary>
public class FaceToCamera : MonoBehaviour
{
    [Header("�Ƕȣ�������Ϊ45��")]
    public float angle;
    private void Start()
    {
        transform.rotation = Quaternion.Euler(angle==0? 45:angle, transform.rotation.y, transform.rotation.z);
    }
    
}
