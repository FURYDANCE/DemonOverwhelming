using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
namespace DemonOverwhelming
{

    /// <summary>
    /// ֱ���ƶ��ű�������ӪΪ��ħʱ����ֱ���ƶ�����֮����
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
                Debug.Log("û����NavMesh����");
                return;
            }
            //��ֱ���ƶ���ͬʱ�������Χ�ĵ��ˣ��Ƿ����׷�����˵�״̬
            //������ִ�е�ԭ�򣺵���Ҳٿ�����ʱ������Χ���е���Ҳ��Ӧ�ý���׷��״̬�������ڲ������κ��ж���ť��ʱ���ҷ�Χ���е��˲Ž���׷��״̬������Ӧ���ھ���Ľű���ִ��
            manager.CheckEnemy();
            manager.SetAttackTarget(manager.enemyCheckScript.EnemyCheck());

            if (entity.camp == Camp.demon)
                moveDir = Vector3.right;
            else
                moveDir = Vector3.left;
            entity.FlipTo(entity.transform.position + moveDir);

            ////����λ��������������״̬ʱ����ƶ�
            //if (!entity.isBackingRelativePos)
            //{
            Vector3 target = new Vector3(transform.position.x + moveDir.x, transform.position.y, transform.position.z );
            //transform.position = Vector3.MoveTowards(transform.position, target, entity.GetSpeed() * Time.deltaTime);
            if (_agent != null/* && (_agent.destination == Vector3.zero || Vector3.Distance(transform.position, _agent.destination) < _resetDestThreshold)*/)
            {
                Debug.Log("������Ŀ��");
                _agent.SetDestination(target);
            }
            entity.FlipTo(target);
            //}
            //����λ������������״̬ʱ����ƶ�
            //if (entity.isBackingRelativePos)
            //{
            //    //Vector3 target = entity.parentSoldierGroup.flagFollowingSoldier.transform.position - entity.OffsetToCaptain;
            //    ////Debug.Log("���ڻص���ʼ����λ��");
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