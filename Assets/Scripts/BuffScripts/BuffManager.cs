using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    public class BuffManager : MonoBehaviour
    {
        [Header("储存的buff信息")]
        public BuffObject buffDatas;
        //public List<BuffInformation> buffDatas;
        /// <summary>
        /// buff管理器单例
        /// </summary>
        public static BuffManager instance;
        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
        }
        /// <summary>
        /// 通过bufftypeid判断应该作用哪种buff子类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BuffBase SetBuffScript(string buffTypeId)
        {
            if (buffTypeId == BuffNames.Buff_AttackStrengthen)
                return new Buff_AttackStrengthen();
            //if (buffTypeId == "4")
            //    return new Buff_WaitForReorganizeTheFormation();
            else
                return null;
        }
        public void RemoveBuffScript(Entity e, string buffTypeId)
        {

        }
        /// <summary>
        /// 通过id找到buff的数据的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BuffInformation findBuffData(string id)
        {
            BuffInformation data = buffDatas.buffs.Find((BuffInformation data) => { return data.buffId == id; });
            if (data != null)
                return data;
            else
                return null;
        }
        /// <summary>
        /// 给实体上buff，需要传入实体和新建一个buffinformation类
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buff"></param>
        public void EntityAddBuff(Entity e, BuffInformation buff)
        {
            e.AddBuff(buff);
        }
        /// <summary>
        /// 给实体上buff，传入实体和需要的buffid即可（前提是这个buff必须在表里面）
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffId"></param>
        public void EntityAddBuff(Entity e, string buffId)
        {

            BuffInformation b = findBuffData(buffId);
            if (b == null)
                return;
            b.buff = SetBuffScript(b.buffTypeId);
            e.AddBuff(b);
            Debug.Log("上buff完成：" + b.buffName);
        }
        /// <summary>
        /// 给实体移除buff
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffId"></param>
        public void EntityRemoveBuff(Entity e, string buffId)
        {
            if (e.buffs.Count != 0)
                for (int i = 0; i < e.buffs.Count; i++)
                    if (e.buffs[i].buffId == buffId)
                    {
                        //执行buff的取消buff方法
                        e.buffs[i].buff.OnRemoveBuff(e, e.buffs[i].buffLevel);
                        RemoveBuffScript(e, buffId);
                        e.buffs.Remove(e.buffs[i]);
                    }

        }
        Debug.Log("上buff完成：" + b.buffName);
    }
}
