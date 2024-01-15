using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
namespace DemonOverwhelming
{
    /// <summary>
    /// 通过给出动画名称，播放单位的骨骼动画的脚本
    /// </summary>
    public class Action_PlayAnimation : Action
    {
        [Header("要播放的动画名称")]
        public string animationName;
        [Header("是否循环播放")]
        public bool loop;
        public SharedEntity thisEntity;
        Entity entity;
        bool played;
        public override void OnStart()
        {

            entity = thisEntity.Value;
            entity.PlayAnimation(animationName, loop);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}