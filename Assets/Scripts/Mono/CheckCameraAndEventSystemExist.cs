using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��ⳡ�����Ƿ���������EventSystem,��������������
/// </summary>
public class CheckCameraAndEventSystemExist : MonoBehaviour
{
    public GameObject cameraPrefab;
    public GameObject eventSystemPrefab;
    private void Awake()
    {
        if (!GameObject.Find("Camera") || !GameObject.Find("EventSystem"))
        {
            Debug.Log("������û��Camera��EventSystem���Զ����ɶ�Ӧ�Ķ���");
            if (!GameObject.Find("Camera"))
                Instantiate(cameraPrefab);
            if (!GameObject.Find("EventSystem"))
                Instantiate(eventSystemPrefab);
        }
    }
}
