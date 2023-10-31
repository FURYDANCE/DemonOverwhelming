using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// ����ĸ����������ĵ�spriteRenender����ΪԤ���������ɱ���ʱ������
    /// </summary>
    public class SoliderGroup : MonoBehaviour
    {
        /// <summary>
        /// չʾ����λ�õ���Ӱ
        /// </summary>
        public Transform[] soldiers;
        public Vector3[] unitOffsetsToCaptain;
        /// <summary>
        /// �����ڳ��������Ҵ��ģ���Ϊ�Ӷ����ʿ��s
        /// </summary>
        public List<Entity> createdSoldiers;
        /// <summary>
        /// ��Ӫ
        /// </summary>
        [HideInInspector]
        public Camp camp;

        public FormatCard parentFormatCard;
        public string finalSoldierId;
        public SpriteRenderer flagSprite;
        /// <summary>
        /// ��ǰ���ĸ���ĵ�λ��Ҳ����һ�ӵĶӳ�
        /// </summary>
        public Entity flagFollowingSoldier;
        /// <summary>
        /// ����������͵Ķ�Ա�������������ж�������������������Ƿ�ȫ�����
        /// </summary>
        int settledUnitAmount;
        /// <summary>
        /// �Ƿ�������������
        /// </summary>
        bool isReorganizing;
        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            CheckUnitBackingToRelativePos();
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void Initialize()
        {
            unitOffsetsToCaptain = new Vector3[soldiers.Length];
            Debug.Log("soldier������" + soldiers.Length);

            //����Ӱ����������������Ч��
            foreach (Transform t in soldiers)
            {
                t.gameObject.AddComponent<FaceToCamera>();
            }
            for (int i = 0; i < soldiers.Length; i++)
            {
                unitOffsetsToCaptain[i] = soldiers[0].transform.position - soldiers[i].transform.position;
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

                UnitData p = GameDataManager.instance.GetEntityDataById(finalSoldierId/*parentFormatCard.parentParameter.soldierId*/);
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
            //SoliderGroup newGroup = flagSprite.gameObject.AddComponent<SoliderGroup>();
            //newGroup = Instantiate(this);
            //Debug.Log(newGroup.gameObject.name);
            //SoliderGroup NG = Instantiate(this.gameObject, this.transform.parent).GetComponent<SoliderGroup>();
            //parentFormatCard.SetConnectGroup(NG);
            //���������id����ʵ��  
            for (int i = 0; i < soldiers.Length; i++)
            {

                Entity e = BattleManager.instance.GenerateOneEntity(camp, parentFormatCard != null ? parentFormatCard.parentParameter.soldierId : finalSoldierId, soldiers[i].position);
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
                //if (destoryShadow)
                Destroy(t.gameObject);
            }
            //����
            //gameObject.AddComponent<FaceToCamera>();
            SetFlagFollowSoldier();
            //���þ���ʿ���Ͷӳ������λ��
            SetUnitOffset();
            //transform.SetParent(createdSoldiers[0].transform);
            //BattleManager.instance.soliderFormatGroups.Remove(this);
            //BattleManager.instance.soliderFormatGroups.Add(NG);

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
        /// <summary>
        /// ���ø���������������λ��
        /// </summary>
        public void SetUnitOffset()
        {
            if (unitOffsetsToCaptain.Length != 0)
                for (int i = 0; i < createdSoldiers.Count; i++)
                {
                    Debug.Log(unitOffsetsToCaptain.Length);
                    if (unitOffsetsToCaptain[i] != null)
                        createdSoldiers[i].OffsetToCaptain = unitOffsetsToCaptain[i];
                }
        }
        /// <summary>
        /// �������������˵�ʿ�������䴦�����ڻص����λ�õ�״̬֮�У�ԭ���и��ӳ��ϼ���buff�����Ǻ���ĳ��˶ӳ���ԭ�صȴ�����ʿ�����
        /// </summary>
        public void SetUnitBackingToRelativePos()
        {
            //BuffManager.instance.EntityAddBuff(createdSoldiers[0], "2000001");
            for (int i = 0; i < createdSoldiers.Count; i++)
            {
                createdSoldiers[i].SetBackingToRelativePos(true);

            }
            settledUnitAmount = 0;
            isReorganizing = true;
        }
        /// <summary>
        /// ���ʿ���Ļص����λ���Ƿ����
        /// </summary>
        public void CheckUnitBackingToRelativePos()
        {
            if (isReorganizing)
            {
                //�������������˵�ʿ���������λ�ú������е����λ�÷ǳ��ӽ�������Ϊ��������Լ����������ͣ���ʱ���ʿ����ԭ�ش��������������ʿ������+1
                //�����������ʿ���������ڴ���ʿ��������ʱ����Ϊ�����������������������һ���ƶ�
                foreach (Entity e in createdSoldiers)
                {
                    if (Vector3.Distance(e.transform.position, e.parentSoldierGroup.createdSoldiers[0].transform.position - e.OffsetToCaptain) < 0.1f)
                    {
                        if (e.GetBackingToRelativePos() == true)
                        {

                            //��ʱ�Ѿ��ص����λ�ã�������ʿ������֮ǰԭ�ش���
                            //if (e != createdSoldiers[0])
                            //{
                            e.SetSpeedBuff(-100);
                            //}

                            //���������ʿ������+1
                            settledUnitAmount++;

                            //Debug.Log("�������ӣ���ǰ��" + settledUnitAmount + "Ŀ�꣺" + createdSoldiers.Count);
                        }
                        //����ʿ���ж�Ϊ������Լ�����������
                        e.SetBackingToRelativePos(false);

                        //Debug.Log("�ص������λ��" + name);


                    }
                    //������ʿ�����룬���ʿ����ԭ�ش���
                    if (settledUnitAmount == createdSoldiers.Count)
                    {
                        foreach (Entity e2 in createdSoldiers)
                        {
                            //if (e2 != createdSoldiers[0])
                            //{
                            Debug.Log("���������" + e2.name);
                            e2.SetSpeedBuff(100);
                            //}
                        }
                        //BuffManager.instance.EntityRemoveBuff(createdSoldiers[0], "2000001");
                        //Debug.Log("����˶ӳ��ļ���buff");

                        //��ʼ����ر���
                        isReorganizing = false;
                        settledUnitAmount = 0;
                    }
                }
            }
        }

        public void SetParentFormatCard(FormatCard card)
        {
            this.parentFormatCard = card;
        }
        public FormatCard GetParentFormatCard()
        {
            return parentFormatCard;
        }
    }
}