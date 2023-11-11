using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemonOverwhelming
{
    /// <summary>
    /// �����ڵ�λ���ϵĴ��������࣬�ڵ�λ����һ��״̬ʱ�ᴥ��������
    /// </summary>
    public class UnitObjectTriggerBase : MonoBehaviour
    {
        Entity thisEntity;
        /// <summary>
        /// �Ƿ񴥷��������������ٴ�����
        /// </summary>
        public bool triggered;
        [Header("��Hpֵ����һ��ֵʱ����")]
        public bool triggerWhenHpLow;
        [Header("����������ʱ����")]
        public bool triggerWhenDestoryOrHide;
        [Header("������HP����")]
        public float triggerHpPercentage;
        float nowHpPercentage;




        void Start()
        {
            thisEntity = GetComponent<Entity>();
        }

        void Update()
        {
            CheckifCanBeTriggered();
        }
        /// <summary>
        /// ����Ƿ���Դ����¼�
        /// </summary>
        public void CheckifCanBeTriggered()
        {
            //�ڰ�ʣ��HP����������£�
            if (triggerWhenHpLow && !triggered)
            {
                nowHpPercentage = (thisEntity.parameter.nowHp / thisEntity.parameter.Hp) * 100;
                if (nowHpPercentage < triggerHpPercentage)
                {
                    OnTrigger();
                    triggered = true;
                }
            }
        }
        private void OnDestroy()
        {
            if (triggerWhenDestoryOrHide && !triggered)
            {
                OnTrigger();
                triggered = true;
            }
        }
        private void OnDisable()
        {
            if (triggerWhenDestoryOrHide && !triggered)
            {
                OnTrigger();
                triggered = true;
            }
        }
        public virtual void OnTrigger()
        {
            Debug.Log("�����˷���");
        }
    }
}