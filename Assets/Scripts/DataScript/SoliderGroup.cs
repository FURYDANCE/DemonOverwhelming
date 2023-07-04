using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ĸ����������ĵ�spriteRenender����ΪԤ���������ɱ���ʱ������
/// </summary>
public class SoliderGroup : MonoBehaviour
{
    /// <summary>
    /// չʾ����λ�õ���Ӱ
    /// </summary>
    public Transform[] soldiers;
    /// <summary>
    /// ��Ϊ�Ӷ����ʿ��s
    /// </summary>
    public List<Entity> createdSoldiers;
    /// <summary>
    /// ��Ӫ
    /// </summary>
    public Camp camp;
    public string Id;
    public SpriteRenderer flagSprite;

    Entity flagFollowingSoldier;
    private void Start()
    {
        //����Ӱ����������������Ч��
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
        //���������id����ʵ��  
        for (int i = 0; i < soldiers.Length; i++)
        {
            Entity e = BattleManager.instance.GenerateOneEntity(camp, Id, soldiers[i].position);
            e.transform.localPosition = new Vector3(e.transform.localPosition.x, e.transform.localPosition.y, 0);
            e.SetParentSoldierGroup(this);
            createdSoldiers.Add(e);

        }
        //����֮��ݻ���Ӱ
        foreach (Transform t in soldiers)
        {
            Destroy(t.gameObject);
        }
        //����
        //gameObject.AddComponent<FaceToCamera>();
        SetFlagFollowSoldier();
        //transform.SetParent(createdSoldiers[0].transform);
    }

    public void OnSoldierDie(Entity diedSoldier)
    {
        flagSprite.GetComponent<Animator>().Play("HurtEffect");
        createdSoldiers.Remove(diedSoldier);
        //ʿ��ȫ������ʱ�ݻ���ض���
        if (createdSoldiers.Count == 0)
        {
            Destroy(flagSprite.gameObject);
            Destroy(gameObject);
        }
        if (diedSoldier == flagFollowingSoldier)
        {
            //�����������ĸ���
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