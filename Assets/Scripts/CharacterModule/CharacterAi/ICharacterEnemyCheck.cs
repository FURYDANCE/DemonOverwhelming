using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// 范围内的敌人检测脚本
    /// </summary>
    public interface ICharacterEnemyCheck
    {
        public Entity EnemyCheck();
    }
}