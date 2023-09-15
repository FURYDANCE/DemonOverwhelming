using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    [System.Serializable]
    public class BuffInformation
    {
        [Header("buff的总id，通过其查找到整个的buff变量")]
        public string buffId;
        [Header("buff的脚本id，通过其判断应生成哪种buff子类")]
        public string buffTypeId;
        public BuffBase buff;
        [Header("buff等级")]
        public float buffLevel;
        [Header("buff名称")]
        public string buffName;
        [Header("buff特效")]
        public GameObject buffVfx;
        [Header("buff持续时间（超过99999则不会计算buff的结束时间）")]
        public float buffTime;

        //构造方法
        public BuffInformation(BuffBase buff, string name, GameObject vfx, float buffLevel, float buffTime)
        {
            if (buffTypeId != "")
            {
                buff = BuffManager.instance.SetBuffScript(buffTypeId);
            }
            this.buff = buff;
            this.buffName = name;
            this.buffVfx = vfx;
            this.buffLevel = buffLevel;
            this.buffTime = buffTime;
        }
        public BuffInformation(string buffId, string name, GameObject vfx, float buffLevel, float buffTime)
        {

            buff = BuffManager.instance.SetBuffScript(buffId);

            this.buffName = name;
            this.buffVfx = vfx;
            this.buffLevel = buffLevel;
            this.buffTime = buffTime;
        }
    }
}