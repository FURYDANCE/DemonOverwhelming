using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{
    /// <summary>
    /// ±ø×éµÄ¸¸¶ÔÏó£¬ÓĞÆìÖÄµÄspriteRenender£¬×÷ÎªÔ¤ÖÆÌåÔÚÉú³É±ø×éÊ±±»µ÷ÓÃ
    /// </summary>
<<<<<<< HEAD
    public class SoliderGroup : MonoBehaviour
=======
    public Transform[] soldiers;
    public Vector3[] unitOffsetsToCaptain;
    /// <summary>
    /// ËùÓĞÔÚ³¡ÄÚÉú³ÉÇÒ´æ»îµÄ£¬×÷Îª×Ó¶ÔÏóµÄÊ¿±øs
    /// </summary>
    public List<Entity> createdSoldiers;
    /// <summary>
    /// ÕóÓª
    /// </summary>
    [HideInInspector]
    public Camp camp;

    public FormatCard parentFormatCard;
    public string finalSoldierId;
    public SpriteRenderer flagSprite;
    /// <summary>
    /// µ±Ç°ÆìÖÄ¸úËæµÄµ¥Î»£¬Ò²¾ÍÊÇÒ»¶ÓµÄ¶Ó³¤
    /// </summary>
    public Entity flagFollowingSoldier;
    /// <summary>
    /// Íê³ÉÖØÕûÕóĞÍµÄ¶ÓÔ±µÄÊıÁ¿£¬ÓÃÓÚÅĞ¶ÏÕû¸ö¶ÓÎéµÄÖØÕûÕóĞÍÊÇ·ñÈ«²¿Íê³É
    /// </summary>
    int settledUnitAmount;
    /// <summary>
    /// ÊÇ·ñÕıÔÚÖØÕûÕóĞÍ
    /// </summary>
    bool isReorganizing;
    private void Start()
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
    {
        /// <summary>
        /// Õ¹Ê¾Éú³ÉÎ»ÖÃµÄĞéÓ°
        /// </summary>
        public Transform[] soldiers;
        public Vector3[] unitOffsetsToCaptain;
        /// <summary>
        /// ËùÓĞÔÚ³¡ÄÚÉú³ÉÇÒ´æ»îµÄ£¬×÷Îª×Ó¶ÔÏóµÄÊ¿±øs
        /// </summary>
        public List<Entity> createdSoldiers;
        /// <summary>
        /// ÕóÓª
        /// </summary>
        [HideInInspector]
        public Camp camp;

        public FormatCard parentFormatCard;
        public string finalSoldierId;
        public SpriteRenderer flagSprite;
        /// <summary>
        /// µ±Ç°ÆìÖÄ¸úËæµÄµ¥Î»£¬Ò²¾ÍÊÇÒ»¶ÓµÄ¶Ó³¤
        /// </summary>
        public Entity flagFollowingSoldier;
        /// <summary>
        /// Íê³ÉÖØÕûÕóĞÍµÄ¶ÓÔ±µÄÊıÁ¿£¬ÓÃÓÚÅĞ¶ÏÕû¸ö¶ÓÎéµÄÖØÕûÕóĞÍÊÇ·ñÈ«²¿Íê³É
        /// </summary>
        int settledUnitAmount;
        /// <summary>
        /// ÊÇ·ñÕıÔÚÖØÕûÕóĞÍ
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
        /// ³õÊ¼»¯
        /// </summary>
        public void Initialize()
        {
<<<<<<< HEAD
            unitOffsetsToCaptain = new Vector3[soldiers.Length];
            Debug.Log("soldierÊıÁ¿£º" + soldiers.Length);

            //¸ø²ĞÓ°¶ÔÏó¼ÓÉÏÃæÏòÏà»úµÄĞ§¹û
            foreach (Transform t in soldiers)
=======
            unitOffsetsToCaptain[i] = soldiers[0].transform.position - soldiers[i].transform.position;
        }
    }
    /// <summary>
    /// ÉèÖÃÆìÖÄÌùÍ¼
    /// </summary>
    /// <param name="sprite"></param>
    public void SetFlagSprite(Sprite sprite)
    {
        flagSprite.sprite = sprite;
    }
    /// <summary>
    /// ÉèÖÃ²ĞÓ°¾ßÌåÌùÍ¼
    /// </summary>
    /// <param name="sprite"></param>
    public void SetSoldierShadowSprite(Sprite sprite)
    {
        foreach (Transform t in soldiers)
        {

            UnitParameter p = GameDataManager.instance.GetEntityDataById(finalSoldierId/*parentFormatCard.parentParameter.soldierId*/);
            t.GetComponent<SpriteRenderer>().sprite = sprite;
            t.transform.localScale = new Vector3(p.modleSize, p.modleSize, 1);
        }
    }
    /// <summary>
    /// Éú³É¾ßÌåµÄÊ¿±ø
    /// </summary>
    /// <param name="destoryShadow">ÊÇ·ñ´İ»Ù³¡ÉÏµÄĞéÓ°</param>
    public void Generate(bool destoryShadow)
    {
        //SoliderGroup newGroup = flagSprite.gameObject.AddComponent<SoliderGroup>();
        //newGroup = Instantiate(this);
        //Debug.Log(newGroup.gameObject.name);
        //SoliderGroup NG = Instantiate(this.gameObject, this.transform.parent).GetComponent<SoliderGroup>();
        //parentFormatCard.SetConnectGroup(NG);
        //¸ù¾İ×ø±êºÍidÉú³ÉÊµÌå  
        for (int i = 0; i < soldiers.Length; i++)
        {

            Entity e = BattleManager.instance.GenerateOneEntity(camp, parentFormatCard != null ? parentFormatCard.parentParameter.soldierId : finalSoldierId, soldiers[i].position);
            if (!e)
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
            {
                t.gameObject.AddComponent<FaceToCamera>();
            }
            for (int i = 0; i < soldiers.Length; i++)
            {
                unitOffsetsToCaptain[i] = soldiers[0].transform.position - soldiers[i].transform.position;
            }
        }
        /// <summary>
        /// ÉèÖÃÆìÖÄÌùÍ¼
        /// </summary>
        /// <param name="sprite"></param>
        public void SetFlagSprite(Sprite sprite)
        {
<<<<<<< HEAD
            flagSprite.sprite = sprite;
        }
        /// <summary>
        /// ÉèÖÃ²ĞÓ°¾ßÌåÌùÍ¼
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
        /// Éú³É¾ßÌåµÄÊ¿±ø
        /// </summary>
        /// <param name="destoryShadow">ÊÇ·ñ´İ»Ù³¡ÉÏµÄĞéÓ°</param>
        public void Generate(bool destoryShadow)
        {
            //SoliderGroup newGroup = flagSprite.gameObject.AddComponent<SoliderGroup>();
            //newGroup = Instantiate(this);
            //Debug.Log(newGroup.gameObject.name);
            //SoliderGroup NG = Instantiate(this.gameObject, this.transform.parent).GetComponent<SoliderGroup>();
            //parentFormatCard.SetConnectGroup(NG);
            //¸ù¾İ×ø±êºÍidÉú³ÉÊµÌå  
            for (int i = 0; i < soldiers.Length; i++)
            {

                Entity e = BattleManager.instance.GenerateOneEntity(camp, parentFormatCard != null ? parentFormatCard.parentParameter.soldierId : finalSoldierId, soldiers[i].position);
                if (!e)
                {
                    Debug.Log("ÊµÌåÃ»ÓĞÕıÈ·Éú³É£¡£¨Ò²ĞíÊÇÉú³ÉµÄid³ö´í»òÆäËûÎÊÌâ£©");
                    foreach (Transform t in soldiers)
                        Destroy(t.gameObject);
                    Destroy(flagSprite.gameObject);
                    return;
                }
                e.transform.localPosition = new Vector3(e.transform.localPosition.x, e.transform.localPosition.y, 0);
                e.SetParentSoldierGroup(this);
                createdSoldiers.Add(e);

            }

            //Éú³ÉÖ®ºó´İ»ÙĞéÓ°
            foreach (Transform t in soldiers)
            {
                //if (destoryShadow)
                Destroy(t.gameObject);
            }
            //¸úËæ
            //gameObject.AddComponent<FaceToCamera>();
=======
            //if (destoryShadow)
                Destroy(t.gameObject);
        }
        //¸úËæ
        //gameObject.AddComponent<FaceToCamera>();
        SetFlagFollowSoldier();
        //ÉèÖÃ¾ßÌåÊ¿±øºÍ¶Ó³¤µÄÏà¶ÔÎ»ÖÃ
        SetUnitOffset();
        //transform.SetParent(createdSoldiers[0].transform);
        //BattleManager.instance.soliderFormatGroups.Remove(this);
        //BattleManager.instance.soliderFormatGroups.Add(NG);

    }
    //µ±×éÄÚÊ¿±øËÀÍöÊ±µ÷ÓÃµÄ·½·¨
    public void OnSoldierDie(Entity diedSoldier)
    {
        flagSprite.GetComponent<Animator>().Play("HurtEffect");
        createdSoldiers.Remove(diedSoldier);
        //Ê¿±øÈ«²¿ËÀÍöÊ±´İ»ÙÏà¹Ø¶ÔÏó
        if (createdSoldiers.Count == 0)
        {
            Destroy(flagSprite.gameObject);
            Destroy(gameObject);
        }
        if (diedSoldier == flagFollowingSoldier)
        {
            //ÖØĞÂÉèÖÃÆìÖÄ¸úËæ
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
            SetFlagFollowSoldier();
            //ÉèÖÃ¾ßÌåÊ¿±øºÍ¶Ó³¤µÄÏà¶ÔÎ»ÖÃ
            SetUnitOffset();
            //transform.SetParent(createdSoldiers[0].transform);
            //BattleManager.instance.soliderFormatGroups.Remove(this);
            //BattleManager.instance.soliderFormatGroups.Add(NG);

        }
        //µ±×éÄÚÊ¿±øËÀÍöÊ±µ÷ÓÃµÄ·½·¨
        public void OnSoldierDie(Entity diedSoldier)
        {
            flagSprite.GetComponent<Animator>().Play("HurtEffect");
            createdSoldiers.Remove(diedSoldier);
            //Ê¿±øÈ«²¿ËÀÍöÊ±´İ»ÙÏà¹Ø¶ÔÏó
            if (createdSoldiers.Count == 0)
            {
                Destroy(flagSprite.gameObject);
                Destroy(gameObject);
            }
            if (diedSoldier == flagFollowingSoldier)
            {
                //ÖØĞÂÉèÖÃÆìÖÄ¸úËæ
                SetFlagFollowSoldier();
            }
        }
        //ÉèÖÃÆìÖÄ¸úËæµÄÊ¿±ø
        void SetFlagFollowSoldier()
        {
            if (createdSoldiers.Count == 0)
                return;
            flagSprite.gameObject.transform.SetParent(createdSoldiers[0].transform);
            flagFollowingSoldier = createdSoldiers[0];
            flagSprite.gameObject.transform.localPosition = new Vector3(0, 8 / createdSoldiers[0].transform.localScale.x, flagSprite.transform.localPosition.z);
        }
        /// <summary>
        /// ÉèÖÃ¸÷¸ö¶ÔÏóµÄÏà¶ÔÕóĞÍÎ»ÖÃ
        /// </summary>
        public void SetUnitOffset()
        {

<<<<<<< HEAD
            for (int i = 0; i < createdSoldiers.Count; i++)
            {
                Debug.Log(unitOffsetsToCaptain.Length);
                if (unitOffsetsToCaptain[i] != null)
                    createdSoldiers[i].OffsetToCaptain = unitOffsetsToCaptain[i];
            }
        }
        /// <summary>
        /// ±éÀúËùÓĞÉú³ÉÁËµÄÊ¿±ø£¬ÈÃÆä´¦ÓÚÕıÔÚ»Øµ½Ïà¶ÔÎ»ÖÃµÄ×´Ì¬Ö®ÖĞ£¨Ô­ÏÈÓĞ¸ø¶Ó³¤ÉÏ¼õËÙbuff£¬µ«ÊÇºóÃæ¸Ä³ÉÁË¶Ó³¤ÔÚÔ­µØµÈ´ıÆäËûÊ¿±ø»ãºÏ
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
        /// ¼ì²âÊ¿±øµÄ»Øµ½Ïà¶ÔÎ»ÖÃÊÇ·ñÍê³É
        /// </summary>
        public void CheckUnitBackingToRelativePos()
        {
            if (isReorganizing)
            {
                //±éÀúËùÓĞÉú³ÉÁËµÄÊ¿±ø£¬Èç¹ûÆäÎ»ÖÃºÍÕóĞÍÖĞµÄÏà¶ÔÎ»ÖÃ·Ç³£½Ó½ü£¬ÔòÊÓÎªËüÍê³ÉÁË×Ô¼ºµÄÖØÕûÕóĞÍ£¬ÕâÊ±Õâ¸öÊ¿±øÔÚÔ­µØ´ıÃü£¬Íê³ÉÖØÕûµÄÊ¿±øÊıÁ¿+1
                //µ±Íê³ÉÖØÕûµÄÊ¿±øÊıÁ¿µÈÓÚ´æ»îµÄÊ¿±ø×ÜÊıÁ¿Ê±£¬ÊÓÎªÕû¸ö¶ÓÎéÍê³ÉÖØÕû£¬¶ÓÎéÔÙÒ»ÆğÒÆ¶¯
                foreach (Entity e in createdSoldiers)
                {
                    if (Vector3.Distance(e.transform.position, e.parentSoldierGroup.createdSoldiers[0].transform.position - e.OffsetToCaptain) < 0.1f)
                    {
                        if (e.GetBackingToRelativePos() == true)
                        {

                            //´ËÊ±ÒÑ¾­»Øµ½Ïà¶ÔÎ»ÖÃ£¬ÔÚËùÓĞÊ¿±øµ½ÆëÖ®Ç°Ô­µØ´ıÃü
                            //if (e != createdSoldiers[0])
                            //{
                            e.SetSpeedBuff(-100);
                            //}

                            //Íê³ÉÖØÕûµÄÊ¿±øÊıÁ¿+1
                            settledUnitAmount++;

                            //Debug.Log("ÊıÁ¿Ôö¼Ó£¡µ±Ç°£º" + settledUnitAmount + "Ä¿±ê£º" + createdSoldiers.Count);
                        }
                        //ÕâÃûÊ¿±øÅĞ¶¨ÎªÍê³ÉÁË×Ô¼ºµÄÖØÕûÕóĞÍ
                        e.SetBackingToRelativePos(false);

                        //Debug.Log("»Øµ½ÁËÏà¶ÔÎ»ÖÃ" + name);
=======
                        //´ËÊ±ÒÑ¾­»Øµ½Ïà¶ÔÎ»ÖÃ£¬ÔÚËùÓĞÊ¿±øµ½ÆëÖ®Ç°Ô­µØ´ıÃü
                        //if (e != createdSoldiers[0])
                        //{
                        e.SetSpeedBuff(-100);
                        //}
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)


                    }
                    //µ±ËùÓĞÊ¿±øµ½Æë£¬½â³ıÊ¿±øµÄÔ­µØ´ıÃü
                    if (settledUnitAmount == createdSoldiers.Count)
                    {
<<<<<<< HEAD
                        foreach (Entity e2 in createdSoldiers)
                        {
                            //if (e2 != createdSoldiers[0])
                            //{
                            Debug.Log("½â³ı´ıÃü£º" + e2.name);
                            e2.SetSpeedBuff(100);
                            //}
                        }
                        //BuffManager.instance.EntityRemoveBuff(createdSoldiers[0], "2000001");
                        //Debug.Log("½â³ıÁË¶Ó³¤µÄ¼õËÙbuff");
=======
                        //if (e2 != createdSoldiers[0])
                        //{
                        Debug.Log("½â³ı´ıÃü£º" + e2.name);
                        e2.SetSpeedBuff(100);
                        //}
                    }
                    //BuffManager.instance.EntityRemoveBuff(createdSoldiers[0], "2000001");
                    //Debug.Log("½â³ıÁË¶Ó³¤µÄ¼õËÙbuff");
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)

                        //³õÊ¼»¯Ïà¹Ø±äÁ¿
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

    public void SetParentFormatCard(FormatCard card)
    {
        this.parentFormatCard = card;
    }
    public FormatCard GetParentFormatCard()
    {
        return parentFormatCard;
    }
}