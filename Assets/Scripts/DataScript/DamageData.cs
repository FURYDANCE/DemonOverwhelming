using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    [System.Serializable]
    public class DamageData
    {
        public string id;
        public string name;
        public float physicDamage;
        public float farDamage;
        public float magicDamage;
        public string[] buffs;
        public string[] specialEffects;
        public string vfxId;
        public float vfxSize;
        public float startDamageWiatTime;

        public DamageData SetDamageData(float physicDamage, float farDamage, float magicDamage)
        {
            this.physicDamage = physicDamage;
            this.farDamage = farDamage;
            this.magicDamage = magicDamage;
            return this;

        }
        public DamageData SetDamageData(float physicDamage, float farDamage, float magicDamage, string[] buffs, string vfxid, string vfxSize)
        {
            this.physicDamage = physicDamage;
            this.farDamage = farDamage;
            this.magicDamage = magicDamage;
            return this;
        }
    }
}