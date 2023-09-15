using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class MoneyBagCheck : MonoBehaviour
    {
        void Update()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 4);
            foreach (Collider c in colliders)
            {
                if (c.tag == Tags.MoneyBag)
                {
                    MoneyBag mb = c.GetComponent<MoneyBag>();
                    mb.StartWork();
                }
            }

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 4);
        }
    }
}