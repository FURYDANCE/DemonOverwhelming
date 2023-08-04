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
    [HideInInspector]
    public Camp camp;
    [HideInInspector]
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
    /// <summary>
    /// ����������ͼ
    /// </summary>
    /// <param name="sprite"></param>
    public void SetFlagSprite(Sprite sprite)
    {
        flagSprite.sprite = sprite;
    }
    /// <summary>
    /// ���ò�Ӱ������ͼ
    /// </summary>
    /// <param name="sprite"></param>
    public void SetSoldierShadowSprite(Sprite sprite)
    {
        foreach (Transform t in soldiers)
        {
            UnitParameter p = GameDataManager.instance.GetEntityDataById(Id);
            t.GetComponent<SpriteRenderer>().sprite = sprite;
            t.transform.localScale = new Vector3(p.modleSize, p.modleSize, 1);
        }
    }
    /// <summary>
    /// ���ɾ����ʿ��
    /// </summary>
    /// <param name="destoryShadow">�Ƿ�ݻٳ��ϵ���Ӱ</param>
    public void Generate(bool destoryShadow)
    {
        //���������id����ʵ��  
        for (int i = 0; i < soldiers.Length; i++)
        {
            Entity e = BattleManager.instance.GenerateOneEntity(camp, Id, soldiers[i].position);
            if (!e)
            {
                Debug.Log("ʵ��û����ȷ���ɣ���Ҳ�������ɵ�id������������⣩");
                foreach (Transform t in soldiers)
                    Destroy(t.gameObject);
                Destroy(flagSprite.gameObject);
                return;
            }
            e.transform.localPosition = new Vector3(e.transform.localPosition.x, e.transform.localPosition.y, 0);
            e.SetParentSoldierGroup(this);
            createdSoldiers.Add(e);

        }
        //����֮��ݻ���Ӱ
        foreach (Transform t in soldiers)
        {
            if (destoryShadow)
                Destroy(t.gameObject);
        }
        //����
        //gameObject.AddComponent<FaceToCamera>();
        SetFlagFollowSoldier();
        //transform.SetParent(createdSoldiers[0].transform);
    }
    //������ʿ������ʱ���õķ���
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
    //�������ĸ����ʿ��
    void SetFlagFollowSoldier()
    {
        if (createdSoldiers.Count == 0)
            return;
        flagSprite.gameObject.transform.SetParent(createdSoldiers[0].transform);
        flagFollowingSoldier = createdSoldiers[0];
        flagSprite.gameObject.transform.localPosition = new Vector3(0, 8 / createdSoldiers[0].transform.localScale.x, flagSprite.transform.localPosition.z);
    }

}