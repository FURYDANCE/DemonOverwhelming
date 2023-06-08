using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ù��ش˽ű��Ķ���������Ӷ����泯�����
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
