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
            thisEntity.enemiesInCheckArea = Physics.OverlapBox(transform.position + (collider.center * thisEntity.transform.localScale.x), collider.size * thisEntity.transform.localScale.x,
                thisEntity.camp == Camp.demon ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, 180, 0), BattleManager.instance.unitLayer);

        }
        private void OnDrawGizmos()
        {
            if (collider)
                Gizmos.DrawWireCube(transform.position + (collider.center * thisEntity.transform.localScale.x), collider.size * thisEntity.transform.localScale.x);
        }
        //用Ontrigger方法，在单位死亡时无法正确执行Exit方法，所以改成了OverlapBox方法

        //private void OnTriggerEnter(Collider collision)
        //{
        //    Entity entity = collision.GetComponent<Entity>();
        //    if (entity && entity.camp != thisEntity.camp)
        //    {
        //        Debug.Log("检测到敌人进入索敌范围");
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