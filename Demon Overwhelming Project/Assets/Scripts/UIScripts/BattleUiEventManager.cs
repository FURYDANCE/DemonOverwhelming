using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUiEventManager : MonoBehaviour
{
    private _EventManager _eventManager;
    CameraBoundCaculate cameraBoundCaculate;
    // Use this for initialization
    void Start()
    {
        _eventManager = GameObject.Find("BattleUIEventManager").GetComponent<_EventManager>() as _EventManager;
        //���¼�
        _eventManager.__AttachEvent("ChangeCameaSliderPersentage", ChangeCameaSliderPersentage);

        cameraBoundCaculate = GetComponentInChildren<CameraBoundCaculate>();
    }
    private void Update()
    {

        if (BattleManager.instance.cameraControlMode == CameraControlMode.followUi)
            cameraBoundCaculate.ChangeCameraPostion();
        else
            cameraBoundCaculate.SetSliderVaule();
    }
    /// <summary>
    /// ���ݻ������ȸı����λ�õķ���
    /// </summary>
    /// <param name="o"></param>
    public void ChangeCameaSliderPersentage(object o)
    {
      

    }
}

