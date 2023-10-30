using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// �����еľݵ�  
    /// </summary>
    public class SceneStrongHold : MonoBehaviour
    {
        [Header("��Ӫ")]
        public Camp camp;
        [Header("�����������Ӫ��λ�͵з���Ӫ�ĳ�����λ")]
        public Transform connectedUnitSpawnPoint_Player;
        public Transform connectedUnitSpawnPoint_Enemy;
        [Header("���ݻ�ʱ��ԭ�ؼ���Ķ��󣨵��˵ľݵ�ݻ�ʱ������Ҿݵ㣬��Ҿݵ�ݻ�ʱ���ɷ��棩")]
        public GameObject gameObjectSetActiveWhenDestory;



        private void OnDestroy()
        {
            BattleManager.instance.CaptureStrongHold(this);
        }



    }
}
