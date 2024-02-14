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
        [Header("Ҫ����������Ϊtrue��false")]
        public bool setBool;
        [Header("Ҫ���õĲ������ͱ���")]
        public bool setSkillInput;
        public bool setCantMove;
        public override void OnStart()
        {
            //��ʼ��
            entity = thisEntity.Value;
            stateManager = GetComponent<CharacterStateManager>();


            //��ֵ
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