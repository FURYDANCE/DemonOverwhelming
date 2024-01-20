using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// ��д��Ͷ������
    /// </summary>
    public class UnitMissile : MonoBehaviour
    {
        /// <summary>
        /// ��Ͷ����Ĵ�����
        /// </summary>
        public Entity creater;
        /// <summary>
        /// ��Ͷ�����Ŀ�������ΪĿ��Ҳ����ʵ�壬��������ΪTransform��
        /// </summary>
        public Transform attackTarget;
        /// <summary>
        /// ���ڻ�ȡ�������ݵ�ID
        /// </summary>
        public string missileId;
        /// <summary>
        /// ��Ͷ����Ŀ������ϻ�ȡ�ĵ�λ���
        /// </summary>
        Entity attackTargetEntity;

        public UnitParameter_Missile parameter;

        /// <summary>
        /// ��ǰλ�Ƶ�Ŀ��λ��
        /// </summary>
        Vector3 nowTarget;

        private void Start()
        {
            SetParameter(creater, missileId, attackTarget);
        }
        private void Update()
        {
            Translate();
            MoveEndCheck();
            if (nowTarget != Vector3.zero)
                RotationCaculate(nowTarget);
        }
        /// <summary>
        /// λ�Ʒ���
        /// </summary>
        public void Translate()
        {
            if (attackTarget == null)
            {
                Debug.Log("Ŀ�겻����");
                MissileDie();
                return;
            }
            //λ��Ŀ����ڵ�λ���ʱ����������λ���ƶ�������ֱ�ӳ���λ��Ŀ��������ƶ�
            if (nowTarget != null)
            {
                if (attackTargetEntity != null)
                    nowTarget = attackTargetEntity.GetUnitCenter().position;
                else
                    nowTarget = attackTarget.position;
            }
            if (parameter.moveType == MissileMoveType.teleport)
            {
                transform.position = nowTarget;
                EndMove();
                return;
            }
         
            transform.position = Vector3.MoveTowards(transform.position, nowTarget, parameter.speed * Time.deltaTime);
        }
        /// <summary>
        /// �����ƶ��ļ��
        /// </summary>
        public void MoveEndCheck()
        {
            if (Vector3.Distance(transform.position, nowTarget) < 0.25f)
            {
                //Debug.Log("�ж�Ͷ�����ƶ����յ�");
                EndMove();
            }
        }
        /// <summary>
        /// ����Ͷ�����ƶ���������Ӧ����
        /// </summary>
        public void EndMove()
        {
            if (attackTargetEntity)
            {
                if (!parameter.useAoe)
                {
                    //Debug.Log("�˺�Ŀ��");
                    //Debug.Log("��ǰ�����vfx�ߴ�1��" + parameter.damageData.vfxSize);

                    BattleManager.instance.CreateDamage(creater, parameter.damageData, attackTargetEntity);
                }
                if (parameter.useAoe)
                {
                    Debug.Log("����aoe�˺�����");
                    BattleManager.instance.CreateAoeHurtArea(attackTargetEntity.transform.position, parameter.aoeArea, creater, creater.camp, parameter.damageData);

                }
                MissileDie();
            }
        }
        //�����Ƕ�
        public void RotationCaculate(Vector3 target)
        {
            if (parameter.moveType == MissileMoveType.parabola)
                return;
            Vector3 v = target - transform.position; //���Ȼ��Ŀ�귽��
            v.z = 0; //����һ��Ҫ��z����Ϊ0
            float angle = Vector3.SignedAngle(Vector3.up, v, Vector3.forward); //�õ�Χ��z����ת�ĽǶ�
            Quaternion rotation = Quaternion.Euler(0, 0, angle); //��ŷ����ת��Ϊ��Ԫ��
            transform.rotation = rotation;
        }
        public void SetParameter(Entity creater, string id, Transform moveTarget)
        {
            this.creater = creater;
            this.attackTarget = moveTarget;
            this.attackTargetEntity = moveTarget.GetComponent<Entity>();
            parameter = GameDataManager.instance.GetMissileDataById(id);
            parameter.damageData = GameDataManager.instance.GetDamageDataById(parameter.damageDataId);
            foreach (BuffInformation buff in creater.buffs)
            {
                buff.buff.OnAddbuff_Missile(this, buff.buffLevel);
            }
        }
        public void MissileDie()
        {
            VfxManager.instance.CreateVfx(parameter.endObjectId, transform.position, new Vector3(5, 5, 5), 5);
            //Debug.Log("Ͷ��������");
            Destroy(gameObject);
            return;
        }
    }
}