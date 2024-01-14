using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
namespace DemonOverwhelming
{

    /// <summary>
    /// 直线移动脚本，当阵营为恶魔时向右直线移动，反之向左
    /// </summary>
    public class ICharaMove_Direct : MonoBehaviour, ICharacterMove
    {
        Entity entity;
        float speed;
        Vector3 moveDir;
        CharacterStateManager manager;
        //float timer;

        void Start()
        {
            entity = GetComponent<Entity>();
            manager = GetComponent<CharacterStateManager>();
            //timer = 0.1f;

            if (!entity.GetComponent<NavMeshAgent>())
                _agent = entity.AddComponent<NavMeshAgent>();
            else
                _agent = entity.GetComponent<NavMeshAgent>();

            _agent.speed = entity.GetSpeed();
            //_agent.SetDestination(Vector3.zero);
        }


        public void Moving()
        {
            if (!_agent.isOnNavMesh)
            {
                Debug.Log("没有在NavMesh上面");
                return;
            }
            //在直线移动的同时，检测周围的敌人，是否进入追击敌人的状态
            //在这里执行的原因：当玩家操控输入时，当范围内有敌人也不应该进入追击状态，而是在不按下任何行动按钮的时候并且范围内有敌人才进入追击状态，所以应该在具体的脚本中执行
            manager.CheckEnemy();
            manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());

            if (entity.camp == Camp.demon)
                moveDir = Vector3.right;
            else
                moveDir = Vector3.left;
            entity.FlipTo(entity.transform.position + moveDir);

            ////当单位不处于重整队形状态时候的移动
            //if (!entity.isBackingRelativePos)
            //{
            Vector3 target = new Vector3(transform.position.x + moveDir.x, transform.position.y, transform.position.z );
            //transform.position = Vector3.MoveTowards(transform.position, target, entity.GetSpeed() * Time.deltaTime);
            if (_agent != null/* && (_agent.destination == Vector3.zero || Vector3.Distance(transform.position, _agent.destination) < _resetDestThreshold)*/)
            {
                Debug.Log("在设置目标");
                _agent.SetDestination(target);
            }
            entity.FlipTo(target);
            //}
            //当单位处于重整队形状态时候的移动
            //if (entity.isBackingRelativePos)
            //{
            //    //Vector3 target = entity.parentSoldierGroup.flagFollowingSoldier.transform.position - entity.OffsetToCaptain;
            //    ////Debug.Log("正在回到初始阵型位置");
            //    //transform.position = Vector3.MoveTowards(transform.position, target, entity.GetSpeed() * Time.deltaTime);
            //    //entity.FlipTo(target);
            //}

            //timer -= Time.deltaTime;
        }

        NavMeshAgent _agent;
        [SerializeField]
        private float _resetDestThreshold = 1f;
    }
}