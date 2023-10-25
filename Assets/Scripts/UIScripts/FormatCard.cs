using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
namespace DemonOverwhelming
{
<<<<<<< HEAD
=======
    public SoldierCardParameter parentParameter;
    /// <summary>
    /// ±ø×é
    /// </summary>
 
    public SoliderGroup connectedSoldierGroup;
    public Shadow connectedShadow;
    /// <summary>
    /// ±ø×é£¨ÔÚ³¡¾°ÖĞÉú³ÉºóµÄ£©
    /// </summary>

    public SoliderGroup connectedSoldierGroup_inScene;
    /// <summary>
    /// ³õÊ¼Î»ÖÃ
    /// </summary>
    Vector3 startPos;
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)

    /// <summary>
    /// ²¼Õó½çÃæÉÏµÄÆìÖÄ¶ÔÏó,ÓĞÍÏ×§¹¦ÄÜ
    /// </summary>
<<<<<<< HEAD
    public class FormatCard : MonoBehaviour
    {
        public SoldierCardParameter parentParameter;
        /// <summary>
        /// ±ø×é
        /// </summary>

        public SoliderGroup connectedSoldierGroup;
        public Shadow connectedShadow;
        /// <summary>
        /// ±ø×é£¨ÔÚ³¡¾°ÖĞÉú³ÉºóµÄ£©
        /// </summary>

        public SoliderGroup connectedSoldierGroup_inScene;
        /// <summary>
        /// ³õÊ¼Î»ÖÃ
        /// </summary>
        Vector3 startPos;

        /// <summary>
        /// ÍÏ×§UI½Å±¾
        /// </summary>
        UiDrag drag;
        Sprite flagSprite;

        public bool noOffset;
        public float x, y;
        private void Start()
=======
    UiDrag drag;
    Sprite flagSprite;

    public bool noOffset;
    public float x, y;
    private void Start()
    {
       
        drag = GetComponent<UiDrag>();
        //Éú³É²ĞÓ°
        connectedSoldierGroup_inScene = Instantiate(connectedSoldierGroup, GameObject.Find("FaceToCamera").transform);
        connectedSoldierGroup_inScene.transform.position = SceneObjectsManager.instance.playerEntityGeneratePoint.transform.position;
        //ÉèÖÃ±äÁ¿
        connectedSoldierGroup_inScene.SetParentFormatCard(this);
        //ÉèÖÃ²ĞÓ°ÔÚÉú³ÉÊµÌåÊ¿±øÊ±µÄÆìÖÄÌùÍ¼
        connectedSoldierGroup_inScene.SetFlagSprite(flagSprite);
        //ÉèÖÃ²ĞÓ°µÄ¾ßÌåÌùÍ¼
        connectedSoldierGroup_inScene.SetSoldierShadowSprite(parentParameter.sprite);

        //½«¸ÃÆìÖÄ¶ÔÓ¦µÄ±ø×éÎ»ÖÃ´«¸øÕ½¶·¹ÜÀíÆ÷
        BattleManager.instance.soliderFormatGroups.Add(connectedSoldierGroup_inScene);
        startPos = connectedSoldierGroup_inScene.transform.position;
        if (!noOffset)
        {
            Debug.LogError("ÓĞOFFSET£¡");
            //ÉèÖÃÆìÖÄµÄÎ»ÖÃÆ«ÒÆ
            x = Random.Range(-80, 80);
            y = Random.Range(-50, 50);

          

        }
        transform.position += new Vector3(x, y, 0);
        drag.startPos = transform.position - new Vector3(x, y, 0);
    }
    private void Update()
    {
        //¼ÆËãuiÍÏ×§µÄÏà¶ÔÎ»ÖÃ£¬¸Ä±ä²ĞÓ°µÄÎ»ÖÃ
        if (connectedSoldierGroup_inScene)
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
        {

            drag = GetComponent<UiDrag>();
            //Éú³É²ĞÓ°
            connectedSoldierGroup_inScene = Instantiate(connectedSoldierGroup, SceneObjectsManager.instance.allUnitParent);
            connectedSoldierGroup_inScene.transform.position = SceneObjectsManager.instance.playerEntityGeneratePoint.transform.position;
            //ÉèÖÃ±äÁ¿
            connectedSoldierGroup_inScene.SetParentFormatCard(this);
            //ÉèÖÃ²ĞÓ°ÔÚÉú³ÉÊµÌåÊ¿±øÊ±µÄÆìÖÄÌùÍ¼
            connectedSoldierGroup_inScene.SetFlagSprite(flagSprite);
            //ÉèÖÃ²ĞÓ°µÄ¾ßÌåÌùÍ¼
            connectedSoldierGroup_inScene.SetSoldierShadowSprite(parentParameter.sprite);

            //½«¸ÃÆìÖÄ¶ÔÓ¦µÄ±ø×éÎ»ÖÃ´«¸øÕ½¶·¹ÜÀíÆ÷
            BattleManager.instance.soliderFormatGroups.Add(connectedSoldierGroup_inScene);
            startPos = connectedSoldierGroup_inScene.transform.position;
            if (!noOffset)
            {

                //ÉèÖÃÆìÖÄµÄÎ»ÖÃÆ«ÒÆ
                x = Random.Range(-80, 80);
                y = Random.Range(-50, 50);

            }
            transform.position += new Vector3(x, y, 0);
            drag.startPos = transform.position - new Vector3(x, y, 0);
        }
        private void Update()
        {
            //¼ÆËãuiÍÏ×§µÄÏà¶ÔÎ»ÖÃ£¬¸Ä±ä²ĞÓ°µÄÎ»ÖÃ
            if (connectedSoldierGroup_inScene)
            {
                connectedSoldierGroup_inScene.transform.position = startPos + new Vector3(drag.relativeGap.x * 0.15f, drag.relativeGap.y * 0.35f + 4, 0);
            }
        }
        public void SetParentParameter(SoldierCardParameter parameter)
        {
            parentParameter = parameter;
            if (parameter.flagSprite != null)
                GetComponent<Image>().sprite = parameter.flagSprite;
        }
        public void ClearThis()
        {
            BattleManager.instance.soliderFormatGroups.Remove(connectedSoldierGroup_inScene);
            Destroy(connectedSoldierGroup_inScene.gameObject);
        }
        public void SetFlagSprite(Sprite sprite)
        {
            this.flagSprite = sprite;
        }
        public void SetConnectGroup(SoliderGroup soliderGroup)
        {
            connectedSoldierGroup_inScene = soliderGroup;
        }
    }
<<<<<<< HEAD
}
=======
    public void SetParentParameter(SoldierCardParameter parameter)
    {
        parentParameter = parameter;
        if (parameter.flagSprite != null)
            GetComponent<Image>().sprite = parameter.flagSprite;
    }
    public void ClearThis()
    {
        BattleManager.instance.soliderFormatGroups.Remove(connectedSoldierGroup_inScene);
        Destroy(connectedSoldierGroup_inScene.gameObject);
    }
    public void SetFlagSprite(Sprite sprite)
    {
        this.flagSprite = sprite;
    }
    public void SetConnectGroup(SoliderGroup soliderGroup)
    {
        connectedSoldierGroup_inScene = soliderGroup;
    }
}
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
