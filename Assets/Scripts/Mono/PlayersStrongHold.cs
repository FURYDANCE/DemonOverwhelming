using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
