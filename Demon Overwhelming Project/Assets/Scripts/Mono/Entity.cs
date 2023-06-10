using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 实体类，挂载在场景中所有的实体上（建筑，兵种，英雄）
/// 鼠标悬停在上面时通过改变material来切换显示
/// </summary>
public class Entity : MonoBehaviour
{
    SpriteRenderer sprite;
    
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnMouseOver()
    {
        BattleManager.instance.nowChooseingTarget = gameObject;
        if (BattleManager.instance.nowChoosedTarget != gameObject)
        {
            sprite.material = SceneObjectsManager.instance.materialObject.onMouseCoverMaterial;
        }
    }
    private void OnMouseExit()
    {
        BattleManager.instance.nowChooseingTarget = null;
        if (BattleManager.instance.nowChoosedTarget != gameObject)
        {
            sprite.material = SceneObjectsManager.instance.materialObject.defaultMaterial;
        }
    }
}
