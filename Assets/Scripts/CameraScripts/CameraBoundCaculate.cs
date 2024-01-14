using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DemonOverwhelming;
/// <summary>
/// ���㲢��������ƶ���Χ�Ľű�������һ��һ�ҵ���������߽����������ã�
/// ��һ֡ ͨ���������ұ߽�ľ����Լ����������߽�ľ���ó����λ��������Ļ�İٷֱ�λ�ã����ó��İٷֱ���Ϊslider��ֵ��ʾ��UI�У�
/// ������϶�UI�ϵĽ�����ʱ�������������ֵ����Ӧ�����겢�����긳ֵ�����
/// ��Ϊ�������Ҫ����ǰ���ƶ�������ֻ���㲢����X�����꼴��
/// </summary>
public class CameraBoundCaculate : MonoBehaviour
{
    public Transform cameraTransform;
    public float bound_Left;
    public float bound_Right;

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

        //��һ֡���㲢��UI��ֵ
        SetSliderVaule();
        slider.onValueChanged.AddListener(delegate
        {
            ChangeCameraPostion();
        });

    }
    private void Update()
    {

    }


    /// <summary>
    /// �������λ�����û�����ֵ
    /// </summary>
    public void SetSliderVaule()
    {
        boundDistance = Mathf.Abs(bound_Right - bound_Left);
        cameraDistance = Mathf.Abs(bound_Left - cameraTransform.position.x);
        cameraPersentage = cameraDistance / boundDistance;
        slider.value = cameraPersentage;
    }


    /// <summary>
    /// ����slider��ֵ�ı������X��λ��
    /// </summary>
    public void ChangeCameraPostion()
    {
        float nowPersentage = slider.value;
        float newPos_X = /*bound_Right -*/ boundDistance * nowPersentage;
        cameraTransform.position = new Vector3(newPos_X + bound_Left, cameraTransform.position.y, cameraTransform.position.z);
    }
}
