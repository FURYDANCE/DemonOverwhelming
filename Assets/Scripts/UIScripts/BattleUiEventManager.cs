using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DemonOverwhelming
{


    public class BattleUiEventManager : MonoBehaviour
    {
        private _EventManager _eventManager;
        CameraBoundCaculate cameraBoundCaculate;
        // Use this for initialization
        void Start()
        {
            _eventManager = GameObject.Find("BattleUIEventManager").GetComponent<_EventManager>() as _EventManager;
            //���¼�
            _eventManager.__AttachEvent("GenerateSoldiers", GenerateSoldiers);
            _eventManager.__AttachEvent("RevokeCardSelect", RevokeCardSelect);
            _eventManager.__AttachEvent("RevokeAllCardSelect", RevokeAllCardSelect);

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
        /// <summary>
        /// ���ˮ�������ɱ��ֵķ���
        /// </summary>
        public void GenerateSoldiers(object o)
        {
            BattleManager.instance.GenerateSoldiers();
        }
        public void RevokeCardSelect(object o)
        {
            BattleManager.instance.RevokeCardSelect();
        }
        public void RevokeAllCardSelect(object o)
        {
            BattleManager.instance.RevokeAllCardSelect();
        }
    }

}