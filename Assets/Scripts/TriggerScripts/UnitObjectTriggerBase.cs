using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemonOverwhelming
{
    /// <summary>
    /// 挂载在单位身上的触发器基类，在单位到达一定状态时会触发（？）
    /// </summary>
    public class UnitObjectTriggerBase : MonoBehaviour
    {
        Entity thisEntity;
        /// <summary>
        /// 是否触发过（触发过则不再触发）
        /// </summary>
        public bool triggered;
        [Header("在Hp值地狱一定值时触发")]
        public bool triggerWhenHpLow;
        [Header("触发的HP比例")]
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
        /// 检测是否可以触发事件
        /// </summary>
        public void CheckifCanBeTriggered()
        {
            //在按剩余HP比例的情况下：
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
        public virtual void OnTrigger()
        {
            Debug.Log("触发了方法");
        }
    }
}