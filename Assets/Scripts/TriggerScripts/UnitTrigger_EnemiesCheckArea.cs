using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemonOverwhelming
{
    public class UnitTrigger_EnemiesCheckArea : MonoBehaviour
    {
        public Entity thisEntity;
        [SerializeField]
        BoxCollider collider;
        float offsetX;
        Vector3 offset;


        private void Start()
        {
            if (!thisEntity)
                thisEntity = GetComponentInParent<Entity>();
            collider = GetComponent<BoxCollider>();
            collider.isTrigger = true;

            thisEntity.enemiesCheckArea = this;
        }
        private void Update()
        {
            //�Ľ���ͬ��Ӫ֮�����������
            offsetX = collider.center.x * thisEntity.transform.localScale.x;
            offset = new Vector3(thisEntity.camp == Camp.demon ? offsetX : -offsetX
                 , collider.center.y * thisEntity.transform.localScale.y, collider.center.z * thisEntity.transform.localScale.z);

            thisEntity.enemiesInCheckArea = Physics.OverlapBox(
                transform.position + offset, collider.size * thisEntity.transform.localScale.x, Quaternion.Euler(0, 0, 0), BattleManager.instance.unitLayer);

        }
        private void OnDrawGizmos()
        {
            if (collider)
            {
                offsetX = collider.center.x * thisEntity.transform.localScale.x;
                offset = new Vector3(thisEntity.camp == Camp.demon ? offsetX : -offsetX
                     , collider.center.y * thisEntity.transform.localScale.y, collider.center.z * thisEntity.transform.localScale.z);
                Gizmos.DrawWireCube(transform.position + offset, collider.size * thisEntity.transform.localScale.x);
            }
        }
        //��Ontrigger�������ڵ�λ����ʱ�޷���ȷִ��Exit���������Ըĳ���OverlapBox����

        //private void OnTriggerEnter(Collider collision)
        //{
        //    Entity entity = collision.GetComponent<Entity>();
        //    if (entity && entity.camp != thisEntity.camp)
        //    {
        //        Debug.Log("��⵽���˽������з�Χ");
        //        thisEntity.enemiesInCheckArea.Add(entity);
        //    }

        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    Entity entity = other.GetComponent<Entity>();
        //    if (thisEntity.enemiesInCheckArea.Contains(entity))
        //    {
        //        thisEntity.enemiesInCheckArea.Remove(entity);
        //    }
        //}
    }
}