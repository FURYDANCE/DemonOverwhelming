using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// 攻击行为接口：普通：进行一次攻击之后，回到行动状态
    /// 游击等攻击方式之后也在这里实现
    /// </summary>
    public interface ICharacterAttack
    {
        public void Attack();
    }
}