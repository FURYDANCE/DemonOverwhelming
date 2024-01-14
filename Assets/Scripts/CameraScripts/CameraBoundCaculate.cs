using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using DemonOverwhelming;
/// <summary>
/// 计算并控制相机移动范围的脚本，给定一左一右的两个相机边界和相机的引用，
/// 第一帧 通过计算左右边界的距离以及相机距离左边界的距离得出相机位于整个屏幕的百分比位置，将得出的百分比作为slider的值显示在UI中，
/// 当玩家拖动UI上的进度条时，计算进度条的值所对应的坐标并将坐标赋值给相机
/// 因为相机不需要上下前后移动，所以只计算并更新X轴坐标即可
/// </summary>
public class CameraBoundCaculate : MonoBehaviour
{
    public Transform cameraTransform;
    public float bound_Left;
    public float bound_Right;

    [Header("需要显示的sliderUI")]
    public Slider slider;
    /// <summary>
    /// 左右边界的距离
    /// </summary>
    float boundDistance;
    /// <summary>
    /// 相机到左边界的距离
    /// </summary>
    float cameraDistance;
    /// <summary>
    /// 目前相机在整个相机移动轴上的百分比
    /// </summary>
    float cameraPersentage;


    // Start is called before the first frame update
    void Start()
    {

        //第一帧计算并给UI赋值
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
    /// 根据相机位置设置滑条的值
    /// </summary>
    public void SetSliderVaule()
    {
        boundDistance = Mathf.Abs(bound_Right - bound_Left);
        cameraDistance = Mathf.Abs(bound_Left - cameraTransform.position.x);
        cameraPersentage = cameraDistance / boundDistance;
        slider.value = cameraPersentage;
    }


    /// <summary>
    /// 根据slider的值改变相机的X轴位置
    /// </summary>
    public void ChangeCameraPostion()
    {
        float nowPersentage = slider.value;
        float newPos_X = /*bound_Right -*/ boundDistance * nowPersentage;
        cameraTransform.position = new Vector3(newPos_X + bound_Left, cameraTransform.position.y, cameraTransform.position.z);
    }
}
