using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

/// <summary>
/// ���㲢��������ƶ���Χ�Ľű�������һ��һ�ҵ���������߽����������ã�
/// ��һ֡ ͨ���������ұ߽�ľ����Լ����������߽�ľ���ó����λ��������Ļ�İٷֱ�λ�ã����ó��İٷֱ���Ϊslider��ֵ��ʾ��UI�У�
/// ������϶�UI�ϵĽ�����ʱ�������������ֵ����Ӧ�����겢�����긳ֵ�����
/// ��Ϊ�������Ҫ����ǰ���ƶ�������ֻ���㲢����X�����꼴��
/// </summary>
public class CameraBoundCaculate : MonoBehaviour
{
  
     Transform bound_Left;
     Transform bound_Right;
     Transform cameraTransform;
    [Header("��Ҫ��ʾ��sliderUI")]
    public Slider slider;
    /// <summary>
    /// ���ұ߽�ľ���
    /// </summary>
    float boundDistance;
    /// <summary>
    /// �������߽�ľ���
    /// </summary>
    float cameraDistance;
    /// <summary>
    /// Ŀǰ�������������ƶ����ϵİٷֱ�
    /// </summary>
    float cameraPersentage;

    
    // Start is called before the first frame update
    void Start()
    {
        //�ӹ������л�ȡ��������Ķ���
       
        SceneObjectsManager.instance.GetCameraBound(out bound_Left,out bound_Right);
        cameraTransform = SceneObjectsManager.instance.GetCamera();
        //��һ֡���㲢��UI��ֵ
        boundDistance = Mathf.Abs(bound_Left.position.x - bound_Right.position.x);
        cameraDistance = Mathf.Abs(bound_Left.position.x - cameraTransform.position.x);
        cameraPersentage = cameraDistance / boundDistance;
        slider.value = cameraPersentage;

      
    }
    /// <summary>
    /// ����slider��ֵ�ı������X��λ��
    /// </summary>
    public void ChangeCameraPostion()
    {
        float nowPersentage = slider.value;
        float newPos_X = bound_Left.position.x + boundDistance * nowPersentage;
        cameraTransform.position = new Vector3(newPos_X, cameraTransform.position.y, cameraTransform.position.z);
    }
}
