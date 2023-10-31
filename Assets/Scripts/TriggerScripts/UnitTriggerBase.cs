using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// �����еĴ���������
    /// </summary>
    public class UnitTriggerBase : MonoBehaviour
    {
        [Header("���������������")]
        /// <summary>
        /// ���봥������ʵ��
        /// </summary>
        [Header("���봥������ʵ��")]
        public Entity triggeredEntity;

        /// <summary>
        /// �ڶ�Ӧ��Ӫ��ʵ�����ʱ����
        /// </summary>
        [Header("�ڶ�Ӧ��Ӫ��ʵ�����ʱ����")]
        public Camp triggerNeedCamp;
        /// <summary>
        /// �Ƿ�ֻ��Ӣ�ۿ��Դ���
        /// </summary>
        [Header("�Ƿ�ֻ��Ӣ�ۿ��Դ���")]
        public bool onlyHeroTrigger;
        /// <summary>
        /// �Ƿ���Զ����
        /// </summary>
        [Header("�Ƿ���Զ����")]
        public bool triggerForever;
        /// <summary>
        /// ������Զ����������£���������������ô���֮���ٴ�����
        /// </summary>
        [Header("������Զ����������£���������������ô���֮���ٴ�����")]
        public float triggerAmount;
        /// <summary>
        /// ����֮����һ�δ����ȴ���ʱ��
        /// </summary>
        [Header("����֮����һ�δ����ȴ���ʱ��")]
        public float triggerWaitTime;
      
        bool canTrigger;
        float triggerWaitTimer;
        float nowTriggerAmount;
        private void Start()
        {
            //��ʼ��
            triggerWaitTimer = triggerWaitTime;
            canTrigger = true;
        }
        private void Update()
        {
            //������ȴʱ��ļ���
            if (!canTrigger)
            {
                triggerWaitTimer -= Time.deltaTime;
                if (triggerWaitTimer <= 0)
                {
                    canTrigger = true;
                    triggerWaitTimer = triggerWaitTime;
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("�ж���������Ӵ");
            //�ڲ��������ô������ҵ�ǰ��������������󴥷�����ʱ�����ٴ���
            if (!triggerForever && nowTriggerAmount >= triggerAmount)
            {
                Debug.Log("�Ѿ��ﵽ����������󴥷�����");
                return;
            }
            Entity e = other.GetComponent<Entity>();
            if (!e)
                return;
            //�����봥�����ĵ�λ��Ӫ��������Ӫƥ��ʱ
            if (e.camp == triggerNeedCamp)
            {
                //��������Ӣ�۵��ж�����������Ӣ�۲��ҽ���ĵ�λ����Ӣ��ʱ����
                if (onlyHeroTrigger && e.isHero == false)
                {
                    Debug.Log("ֻ��Ӣ�ۿ��Դ����ô�����");
                    return;
                }
                //ִ�е�����˵���������еĴ�����������canTriggerΪ��ʱ�����д�������canTrigger����Ϊfalse����������++
                if (canTrigger)
                {
                    OnTrigger(other);
                    canTrigger = false;
                    nowTriggerAmount++;
                    //Ҳ���ô�����ʵ�壨������Դ����
                    triggeredEntity = e;
                }
            }
        }


        public virtual void OnTrigger(Collider collision)
        {
            Debug.Log("�е�λ���봥������");
        }

    }
}