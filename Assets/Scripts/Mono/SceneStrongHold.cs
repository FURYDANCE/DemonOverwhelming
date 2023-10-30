using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// 场景中的据点  
    /// </summary>
    public class SceneStrongHold : MonoBehaviour
    {
        [Header("阵营")]
        public Camp camp;
        [Header("关联的玩家阵营单位和敌方阵营的出生点位")]
        public Transform connectedUnitSpawnPoint_Player;
        public Transform connectedUnitSpawnPoint_Enemy;
        [Header("当摧毁时在原地激活的对象（敌人的据点摧毁时生成玩家据点，玩家据点摧毁时生成废墟）")]
        public GameObject gameObjectSetActiveWhenDestory;



        private void OnDestroy()
        {
            BattleManager.instance.CaptureStrongHold(this);
        }



    }
}
