using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 兵组的父对象，有旗帜的spriteRenender，作为预制体在生成兵组时被调用
/// </summary>
public class SoliderGroup : MonoBehaviour
{
    /// <summary>
    /// 展示生成位置的虚影
    /// </summary>
    public Transform[] soldiers;
    /// <summary>
    /// 作为子对象的士兵s
    /// </summary>
    public List<Entity> createdSoldiers;
    /// <summary>
    /// 阵营
    /// </summary>
    public Camp camp;
    public string Id;
    public SpriteRenderer flagSprite;

    Entity flagFollowingSoldier;
    private void Start()
    {
        //给残影对象加上面向相机的效果
        foreach (Transform t in soldiers)
        {
            t.gameObject.AddComponent<FaceToCamera>();
        }
    }
    public void SetFlagSprite(Sprite sprite)
    {
        flagSprite.sprite = sprite;
    }
    public void Generate()
    {
        //根据坐标和id生成实体  
        for (int i = 0; i < soldiers.Length; i++)
        {
            Entity e = BattleManager.instance.GenerateOneEntity(camp, Id, soldiers[i].position);
            e.transform.localPosition = new Vector3(e.transform.localPosition.x, e.transform.localPosition.y, 0);
            e.SetParentSoldierGroup(this);
            createdSoldiers.Add(e);

        }
        //生成之后摧毁虚影
        foreach (Transform t in soldiers)
        {
            Destroy(t.gameObject);
        }
        //跟随
        //gameObject.AddComponent<FaceToCamera>();
        SetFlagFollowSoldier();
        //transform.SetParent(createdSoldiers[0].transform);
    }

    public void OnSoldierDie(Entity diedSoldier)
    {
        flagSprite.GetComponent<Animator>().Play("HurtEffect");
        createdSoldiers.Remove(diedSoldier);
        //士兵全部死亡时摧毁相关对象
        if (createdSoldiers.Count == 0)
        {
            Destroy(flagSprite.gameObject);
            Destroy(gameObject);
        }
        if (diedSoldier == flagFollowingSoldier)
        {
            //重新设置旗帜跟随
            SetFlagFollowSoldier();
        }
    }

    void SetFlagFollowSoldier()
    {
        if (createdSoldiers.Count == 0)
            return;
        flagSprite.gameObject.transform.SetParent(createdSoldiers[0].transform);
        flagFollowingSoldier = createdSoldiers[0];
        flagSprite.gameObject.transform.localPosition = new Vector3(0, 8 / createdSoldiers[0].transform.localScale.x, flagSprite.transform.localPosition.z);
    }

}