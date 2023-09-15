using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    public class Buff_WaitForReorganizeTheFormation : BuffBase
    {
        float startSpeed;
        float lateSpeed;
        public void OnAddBuff(Entity e, float buffLevel)
        {
            Debug.Log("…œ¡Àbuff");
            e.SetSpeedBuff(-100);
        }

        public void OnAddbuff_Missile(Missile m, float buffLevel)
        {

        }

        public void OnRemoveBuff(Entity e, float buffLevel)
        {

            e.SetSpeedBuff(100);

        }

        public void OnUpdateBuff(Entity e, float buffLevel)
        {

        }


    }
}