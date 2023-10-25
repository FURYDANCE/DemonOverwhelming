using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// 角色移动种类接口：直行，玩家按键控制，鼠标点击位置(*这种还有问题）
    /// </summary>
    public interface ICharacterMove
    {
        public void Moving();

    }
}