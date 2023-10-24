using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
<<<<<<< HEAD
namespace DemonOverwhelming
=======
/// <summary>
/// ´æ·Å³¡¾°ÖĞĞèÒª±»µ÷ÓÃµÄ¶ÔÏó
/// ¹ÜÀí³¡¾°ÖĞ¿ÉÄÜ±»ÆäËûÀàµ÷ÓÃµÄ¶ÔÏóµÄ¹ÜÀíÆ÷£¬ĞèÒªµ÷ÓÃÊ±µ÷ÓÃÆäÖĞµÄget·½·¨
/// Ë¼Â·ÊÇ£¬ÄÑÃâ»áÓöµ½ĞèÒªÆäËûÀàµ÷ÓÃµÄ¶ÔÏó£¬±ÈÈç¼ÆËãUI¿ÉÄÜĞèÒªÏà»úÏà¹ØµÄ±äÁ¿£¬´ÓÕâ¸öµ¥ÀıÖĞ»ñÈ¡µ½ËùĞèµÄ¶ÔÏó¸üºÃËİÔ´£¿
/// </summary>

public class SceneObjectsManager : MonoBehaviour
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
{
    /// <summary>
    /// ´æ·Å³¡¾°ÖĞĞèÒª±»µ÷ÓÃµÄ¶ÔÏó
    /// ¹ÜÀí³¡¾°ÖĞ¿ÉÄÜ±»ÆäËûÀàµ÷ÓÃµÄ¶ÔÏóµÄ¹ÜÀíÆ÷£¬ĞèÒªµ÷ÓÃÊ±µ÷ÓÃÆäÖĞµÄget·½·¨
    /// Ë¼Â·ÊÇ£¬ÄÑÃâ»áÓöµ½ĞèÒªÆäËûÀàµ÷ÓÃµÄ¶ÔÏó£¬±ÈÈç¼ÆËãUI¿ÉÄÜĞèÒªÏà»úÏà¹ØµÄ±äÁ¿£¬´ÓÕâ¸öµ¥ÀıÖĞ»ñÈ¡µ½ËùĞèµÄ¶ÔÏó¸üºÃËİÔ´£¿
    /// </summary>

<<<<<<< HEAD
    public class SceneObjectsManager : MonoBehaviour
    {

        [Header("½ğÇ®text")]
        public TextMeshProUGUI moneyText;
        [Header("costText")]
        public TextMeshProUGUI bloodText;
        [Header("ËùÓĞÊµÌåµÄ¸¸¶ÔÏó")]
        public Transform allUnitParent;
        [Header("ÊµÌåÉú³ÉµãÎ»")]
        public Transform playerEntityGeneratePoint;
        public Transform enemyEntityGeneratePoint;
        [Header("ÑªÌõUI")]
        public GameObject hpBarObject;
        [Header("Ïà»ú")]
        [SerializeField]
        private CinemachineVirtualCamera camera;
        /// <summary>
        /// ÕâÊÇÏà»ú¸úËæµÄÄ¿±ê£¬¿ÉÒÔ¿´×öÏà»úµÄÒÆ¶¯Öá£¬ÔÚbattlemanagerÖĞ£¬µ±Ñ¡ÔñÁËÊµÌåÖ®ºó¼ÆËãËüµÄ¶ÔÓ¦µÄÎ»ÖÃ£¬²»»áÓĞyºÍzÖáµÄÔË¶¯£¬Ö»¸Ä±äxÖá
        /// </summary>
        [Header("Ïà»úµÄ¸úËæµãÎ»")]
        [SerializeField]
        private Transform cameraFollowTarget;
        [Header("Ïà»úµÄ×óÓÒ±ß½ç")]
        [SerializeField]
        public Transform cameraBound_Left;
        [SerializeField]
        public Transform cameraBound_Right;
        [Header("½ğÇ®ºÍcostµÄÌî³ä")]
        public Image moneyFill;
        public Image costFill;
        [Header("Êó±êĞüÍ£Ê±µÄ¶ÔÏóĞÅÏ¢ÏÔÊ¾UI")]
        public ObjectInfoUI objectInfoUI;
        [Header("Íæ¼Ò×îÖÕ¾İµã")]
        public GameObject playerFinalStrongHold;
        [Header("GAMEOVER½çÃæ")]
        public GameOverUI gameOverPanel;
        [Header("Õ½¶·½çÃæUI")]
        [Header("UIÏà¹Øs")]
        public GameObject BattleUI;
        [Header("Ñ¡¿¨½çÃæ")]
        public Image cardSelectUI;
        [Header("²¼Õó½çÃæ")]
        public Image formationMakingUI;
        [Header("Ó¢ĞÛĞÅÏ¢½çÃæ")]
        public HeroInformationUI heroInfoUI;
        public static SceneObjectsManager instance;
        private void Awake()
        {
            if (instance != null)
                Destroy(instance);
            instance = this;
            Initialize();

        }
        public void Initialize()
        {
            if (camera == null)
                camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
            if (cameraBound_Left == null)
                cameraBound_Left = GameObject.Find("LeftCameraBound").transform;
            if (cameraBound_Right == null)
                cameraBound_Right = GameObject.Find("RightCameraBound").transform;
            if (playerEntityGeneratePoint == null)
                playerEntityGeneratePoint = GameObject.Find("EntityGenerateArea_Left").transform;
            if (enemyEntityGeneratePoint == null)
                enemyEntityGeneratePoint = GameObject.Find("EntityGenerateArea_Right").transform;
            if (moneyFill == null)
                moneyFill = GameObject.Find("MoneyFill").GetComponent<Image>();
            if (costFill == null)
                costFill = GameObject.Find("BolldFill").GetComponent<Image>();
            if (objectInfoUI == null)
                objectInfoUI = GameObject.Find("ObjectInfoUI").GetComponent<ObjectInfoUI>();
            if (playerFinalStrongHold == null)
                playerFinalStrongHold = GameObject.Find("Player's Stronghold");
            if (gameOverPanel == null)
                gameOverPanel = GameObject.Find("GameOverUi").GetComponent<GameOverUI>();
            if (!BattleUI)
                BattleUI = GameObject.Find("BattleUI");
            if (!cardSelectUI)
                cardSelectUI = GameObject.Find("CardSelectArea").GetComponent<Image>();
            if (!formationMakingUI)
                formationMakingUI = GameObject.Find("FormationMakingArea").GetComponent<Image>();
            if (!heroInfoUI)
                heroInfoUI = GameObject.Find("HeroInfoArea").GetComponent<HeroInformationUI>();
        }

        /// <summary>
        /// »ñÈ¡Ïà»ú¶ÔÏó
        /// </summary>
        /// <returns></returns>
        public Transform GetCamera()
        {
            return camera.transform;
        }
        public CinemachineVirtualCamera GetCameraScript()
        {
            return camera;
        }
        public Transform GetCameraFollowTarget()
        {
            return cameraFollowTarget;
        }
        /// <summary>
        /// »ñÈ¡Ïà»úµÄ×óÓÒ±ß½ç
        /// </summary>
        /// <param name="L"></param>
        /// <param name="R"></param>
        public void GetCameraBound(out Transform L, out Transform R)
        {
            L = cameraBound_Left;
            R = cameraBound_Right;
        }

        public void ShowObjectInfoUI(bool isTrue)
        {
            objectInfoUI.gameObject.SetActive(isTrue);
        }
    }
}
=======
    [Header("½ğÇ®text")]
    public TextMeshProUGUI moneyText;
    [Header("costText")]
    public TextMeshProUGUI bloodText;
    [Header("ÊµÌåÉú³ÉµãÎ»")]
    public Transform playerEntityGeneratePoint;
    public Transform enemyEntityGeneratePoint;
    [Header("ÑªÌõUI")]
    public GameObject hpBarObject;
    [Header("Ïà»ú")]
    [SerializeField]
    private CinemachineVirtualCamera camera;
    /// <summary>
    /// ÕâÊÇÏà»ú¸úËæµÄÄ¿±ê£¬¿ÉÒÔ¿´×öÏà»úµÄÒÆ¶¯Öá£¬ÔÚbattlemanagerÖĞ£¬µ±Ñ¡ÔñÁËÊµÌåÖ®ºó¼ÆËãËüµÄ¶ÔÓ¦µÄÎ»ÖÃ£¬²»»áÓĞyºÍzÖáµÄÔË¶¯£¬Ö»¸Ä±äxÖá
    /// </summary>
    [Header("Ïà»úµÄ¸úËæµãÎ»")]
    [SerializeField]
    private Transform cameraFollowTarget;
    [Header("Ïà»úµÄ×óÓÒ±ß½ç")]
    [SerializeField]
    public Transform cameraBound_Left;
    [SerializeField]
    public Transform cameraBound_Right;
    [Header("½ğÇ®ºÍcostµÄÌî³ä")]
    public Image moneyFill;
    public Image costFill;
    [Header("Êó±êĞüÍ£Ê±µÄ¶ÔÏóĞÅÏ¢ÏÔÊ¾UI")]
    public ObjectInfoUI objectInfoUI;
    [Header("Íæ¼Ò×îÖÕ¾İµã")]
    public GameObject playerFinalStrongHold;
    [Header("GAMEOVER½çÃæ")]
    public GameOverUI gameOverPanel;
    [Header("Õ½¶·½çÃæUI")]
    [Header("UIÏà¹Øs")]
    public GameObject BattleUI;
    [Header("Ñ¡¿¨½çÃæ")]
    public Image cardSelectUI;
    [Header("²¼Õó½çÃæ")]
    public Image formationMakingUI;
    [Header("Ó¢ĞÛĞÅÏ¢½çÃæ")]
    public HeroInformationUI heroInfoUI;
    public static SceneObjectsManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
        Initialize();

    }
    public void Initialize()
    {
        if (camera == null)
            camera = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        if (cameraBound_Left == null)
            cameraBound_Left = GameObject.Find("LeftCameraBound").transform;
        if (cameraBound_Right == null)
            cameraBound_Right = GameObject.Find("RightCameraBound").transform;
        if (playerEntityGeneratePoint == null)
            playerEntityGeneratePoint = GameObject.Find("EntityGenerateArea_Left").transform;
        if (enemyEntityGeneratePoint == null)
            enemyEntityGeneratePoint = GameObject.Find("EntityGenerateArea_Right").transform;
        if (moneyFill == null)
            moneyFill = GameObject.Find("MoneyFill").GetComponent<Image>();
        if (costFill == null)
            costFill = GameObject.Find("BolldFill").GetComponent<Image>();
        if (objectInfoUI == null)
            objectInfoUI = GameObject.Find("ObjectInfoUI").GetComponent<ObjectInfoUI>();
        if (playerFinalStrongHold == null)
            playerFinalStrongHold = GameObject.Find("Player's Stronghold");
        if (gameOverPanel == null)
            gameOverPanel = GameObject.Find("GameOverUi").GetComponent<GameOverUI>();
        if (!BattleUI)
            BattleUI = GameObject.Find("BattleUI");
        if(!cardSelectUI)
            cardSelectUI = GameObject.Find("CardSelectArea").GetComponent<Image>();
        if (!formationMakingUI)
            formationMakingUI = GameObject.Find("FormationMakingArea").GetComponent<Image>();
        if(!heroInfoUI)
            heroInfoUI = GameObject.Find("HeroInfoArea").GetComponent<HeroInformationUI>();
    }

    /// <summary>
    /// »ñÈ¡Ïà»ú¶ÔÏó
    /// </summary>
    /// <returns></returns>
    public Transform GetCamera()
    {
        return camera.transform;
    }
    public CinemachineVirtualCamera GetCameraScript()
    {
        return camera;
    }
    public Transform GetCameraFollowTarget()
    {
        return cameraFollowTarget;
    }
    /// <summary>
    /// »ñÈ¡Ïà»úµÄ×óÓÒ±ß½ç
    /// </summary>
    /// <param name="L"></param>
    /// <param name="R"></param>
    public void GetCameraBound(out Transform L, out Transform R)
    {
        L = cameraBound_Left;
        R = cameraBound_Right;
    }

    public void ShowObjectInfoUI(bool isTrue)
    {
        objectInfoUI.gameObject.SetActive(isTrue);
    }
}
>>>>>>> c920aad3 (8.23 ä¿®æ”¹äº†æˆ˜æ–—ç•Œé¢ï¼ŒåŠ å…¥å‡ ä¸ªæ–°å…µç§å¡ï¼ˆç›®å‰å¯ä»¥åŒæ—¶å­˜åœ¨8å¼ å¡ï¼‰ï¼Œä¿®å¤äº†å…µç§ç”Ÿæˆç›¸å…³çš„bug)
