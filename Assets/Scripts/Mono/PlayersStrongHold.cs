using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ¡Ÿ ±µƒ≈–∂œ ‰”ÆΩ≈±æ
/// </summary>
public class PlayersStrongHold : MonoBehaviour
{
    public bool isPlayer;
    private void OnDestroy()
    {
        BattleManager.instance.EndBattle(isPlayer);
    }
}
