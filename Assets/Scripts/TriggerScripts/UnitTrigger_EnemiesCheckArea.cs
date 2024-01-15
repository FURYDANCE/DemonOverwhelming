using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemonOverwhelming
{
    public class UnitTrigger_EnemiesCheckArea : MonoBehaviour
    {
        public Entity thisEntity;


        private void Start()
        {
            if (!thisEntity)
                thisEntity = GetComponentInParent<Entity>();
        }
        private void OnTriggerEnter(Collider collision)
        {
            Entity entity = collision.GetComponent<Entity>();
            if (entity && entity.camp != thisEntity.camp)
            {
                Debug.Log("¼ì²âµ½µÐÈË½øÈëË÷µÐ·¶Î§");
                thisEntity.enemiesInCheckArea.Add(entity);
            }

        }
        private void OnTriggerExit(Collider other)
        {
            Entity entity = other.GetComponent<Entity>();
            if (thisEntity.enemiesInCheckArea.Contains(entity))
            {
                thisEntity.enemiesInCheckArea.Remove(entity);
            }
        }
    }
}