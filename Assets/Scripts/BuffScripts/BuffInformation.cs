using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    [System.Serializable]
    public class BuffInformation
    {
        [Header("buff����id��ͨ������ҵ�������buff����")]
        public string buffId;
        [Header("buff�Ľű�id��ͨ�����ж�Ӧ��������buff����")]
        public string buffTypeId;
        public BuffBase buff;
        [Header("buff�ȼ�")]
        public float buffLevel;
        [Header("buff����")]
        public string buffName;
        [Header("buff��Ч")]
        public GameObject buffVfx;
        [Header("buff����ʱ�䣨����99999�򲻻����buff�Ľ���ʱ�䣩")]
        public float buffTime;

        //���췽��
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