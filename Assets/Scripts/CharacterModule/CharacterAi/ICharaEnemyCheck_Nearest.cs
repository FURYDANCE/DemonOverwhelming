using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    public class ICharaEnemyCheck_Nearest : MonoBehaviour, ICharacterEnemyCheck
    {
        CharacterStateManager manager;
        /// <summary>
        /// �������з�Χ�������Ŀ��ѡȡ׷��Ŀ��
        /// </summary>
        /// <returns></returns>
        public Entity EnemyCheck()
        {
            Entity selected = null;
            if (manager.enemySelected.Count != 0)
            {
                float minDistance = Vector3.Distance(transform.position, manager.enemySelected[0].transform.position);

                foreach (Collider c in manager.enemySelected)
                {
                    if (!c)
                        continue;
                    if (Vector3.Distance(transform.position, c.transform.position) <= minDistance)
                    {
                        minDistance = Vector3.Distance(transform.position, c.transform.position);
                        selected = c.GetComponent<Entity>();
                    }
                }
                return selected;
            }
            else
                return null;
        }


        void Start()
        {
            manager = GetComponent<CharacterStateManager>();
        }


    }
}