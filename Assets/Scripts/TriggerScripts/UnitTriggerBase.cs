using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// 场景中的触发器基类
    /// </summary>
    public class UnitTriggerBase : MonoBehaviour
    {
        [Header("触发器基类变量：")]
        /// <summary>
        /// 进入触发器的实体
        /// </summary>
        [Header("进入触发器的实体")]
        public Entity triggeredEntity;

        /// <summary>
        /// 在对应阵营的实体进入时触发
        /// </summary>
        [Header("在对应阵营的实体进入时触发")]
        public Camp triggerNeedCamp;
        /// <summary>
        /// 是否只有英雄可以触发
        /// </summary>
        [Header("是否只有英雄可以触发")]
        public bool onlyHeroTrigger;
        /// <summary>
        /// 是否永远触发
        /// </summary>
        [Header("是否永远触发")]
        public bool triggerForever;
        /// <summary>
        /// 不是永远触发的情况下：触发次数（到达该次数之后不再触发）
        /// </summary>
        [Header("不是永远触发的情况下：触发次数（到达该次数之后不再触发）")]
        public float triggerAmount;
        /// <summary>
        /// 触发之后到下一次触发等待的时间
        /// </summary>
        [Header("触发之后到下一次触发等待的时间")]
        public float triggerWaitTime;
      
        bool canTrigger;
        float triggerWaitTimer;
        float nowTriggerAmount;
        private void Start()
        {
            //初始化
            triggerWaitTimer = triggerWaitTime;
            canTrigger = true;
        }
        private void Update()
        {
            //触发冷却时间的计算
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
            Debug.Log("有东西进来了哟");
            //在并不是永久触发并且当前触发次数到达最大触发次数时，不再触发
            if (!triggerForever && nowTriggerAmount >= triggerAmount)
            {
                Debug.Log("已经达到触发器的最大触发次数");
                return;
            }
            Entity e = other.GetComponent<Entity>();
            if (!e)
                return;
            //当进入触发器的单位阵营和所需阵营匹配时
            if (e.camp == triggerNeedCamp)
            {
                //检测仅触发英雄的判定：当仅触发英雄并且进入的单位不是英雄时返回
                if (onlyHeroTrigger && e.isHero == false)
                {
                    Debug.Log("只有英雄可以触发该触发器");
                    return;
                }
                //执行到这里说明满足所有的触发条件，当canTrigger为真时，进行触发，将canTrigger设置为false，触发次数++
                if (canTrigger)
                {
                    OnTrigger(other);
                    canTrigger = false;
                    nowTriggerAmount++;
                    //也设置触发的实体（便于溯源？）
                    triggeredEntity = e;
                }
            }
        }


        public virtual void OnTrigger(Collider collision)
        {
            Debug.Log("有单位进入触发区域");
        }

    }
}