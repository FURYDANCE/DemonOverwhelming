using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    /// <summary>
    /// ��ʱ���ж���Ӯ�ű�
    /// </summary>
    public class PlayersStrongHold : MonoBehaviour
    {
        public bool isPlayer;
        private void OnDestroy()
        {
            BattleManager.instance.EndBattle(isPlayer);
        }
    }
}