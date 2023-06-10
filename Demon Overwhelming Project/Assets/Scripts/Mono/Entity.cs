using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ʵ���࣬�����ڳ��������е�ʵ���ϣ����������֣�Ӣ�ۣ�
/// �����ͣ������ʱͨ���ı�material���л���ʾ
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
