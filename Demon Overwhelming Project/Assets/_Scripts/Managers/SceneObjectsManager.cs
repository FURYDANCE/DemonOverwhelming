using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �������п��ܱ���������õĶ���Ĺ���������Ҫ����ʱ�������е�get����
/// ˼·�ǣ������������Ҫ��������õĶ��󣬱������UI������Ҫ�����صı���������������л�ȡ������Ķ��������Դ��
/// </summary>
public class SceneObjectsManager : MonoBehaviour
{
    [Header("���")]
    [SerializeField]
    private Transform camera;
    [Header("��������ұ߽�")]
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
    /// ��ȡ�������
    /// </summary>
    /// <returns></returns>
    public Transform GetCamera()
    {
        return camera;
    }
    /// <summary>
    /// ��ȡ��������ұ߽�
    /// </summary>
    /// <param name="L"></param>
    /// <param name="R"></param>
    public void GetCameraBound(out Transform L, out Transform R)
    {
        L = cameraBound_Left;
        R = cameraBound_Right;
    }
}
