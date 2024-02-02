using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    public class Action_SetParameter : Action
    {
        public  SharedEntity thisEntity;
        Entity entity;
        CharacterStateManager stateManager;
        [Header("要将变量设置为true或false")]
        public bool setBool;
        [Header("要设置的布尔类型变量")]
        public bool setSkillInput;
        public bool setCantMove;
        public override void OnStart()
        {
            //初始化
            entity = thisEntity.Value;
            stateManager = GetComponent<CharacterStateManager>();


            //赋值
            if (setSkillInput)
                stateManager.SetSkillInput(setBool);
            if (setCantMove)
                stateManager.SetCantMove(setBool);


        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}