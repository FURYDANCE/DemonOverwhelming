using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// buff�Ļ���
    /// </summary>
    public interface BuffBase
    {

        /// <summary>
        /// ʵ����buffʱִ�еķ���
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffLevel"></param>
        public void OnAddBuff(Entity e, float buffLevel);
        /// <summary>
        /// ʵ��ȡ��buff��ʱ��ִ�еķ���
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffLevel"></param>
        public void OnRemoveBuff(Entity e, float buffLevel);
        /// <summary>
        /// ʵ�����и�buffʱÿһִ֡�еķ���
        /// </summary>
        /// <param name="e"></param>
        /// <param name="buffLevel"></param>
        public void OnUpdateBuff(Entity e, float buffLevel);
        /// <summary>
        /// ʵ���ڷ���Ͷ����ʱ����Ͷ�������buff���иı�ķ�������Ͷ������������ݷ����У���ȡͶ����Ĵ���������buff֮��ִ�и÷�����
        /// </summary>
        /// <param name="m"></param>
        /// <param name="buffLevel"></param>
        public void OnAddbuff_Missile(UnitMissile m, float buffLevel);
    }
}