using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// buff的基类
    /// </summary>
    public interface BuffBase
    {

        /// <summary>
        /// 实体上buff时执行的方法
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffLevel"></param>
        public void OnAddBuff(Entity e, float buffLevel);
        /// <summary>
        /// 实体取消buff的时候执行的方法
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffLevel"></param>
        public void OnRemoveBuff(Entity e, float buffLevel);
        /// <summary>
        /// 实体在有该buff时每一帧执行的方法
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffLevel"></param>
        public void OnUpdateBuff(Entity e, float buffLevel);
        /// <summary>
        /// 实体在发射投射物时，对投射物根据buff进行改变的方法（在投射物的设置数据方法中，获取投射物的创造者有无buff之后执行该方法）
        /// </summary>
        /// <param name="m"></param>
        /// <param name="buffLevel"></param>
        public void OnAddbuff_Missile(UnitMissile m, float buffLevel);
    }
}