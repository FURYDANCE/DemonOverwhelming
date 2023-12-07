using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace DemonOverwhelming
{

    /// <summary>
    /// 鼠标悬停在兵种卡上时显示信息界面的脚本
    /// </summary>
    public class MouseHover_SoldierCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string[] descriptionAndStory;
        ObjectInfoUI infoUI;

        public void OnPointerEnter(PointerEventData eventData)
        {
            return;
            Debug.Log("鼠标进入");
            infoUI.DestoryAllText();
            SceneObjectsManager.instance.ShowObjectInfoUI(true);
            foreach (string s in descriptionAndStory)
            {
                infoUI.CreateNewText(s);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            return;

            Debug.Log("鼠标退出");

            SceneObjectsManager.instance.ShowObjectInfoUI(false);
        }

        private void Start()
        {
            infoUI = SceneObjectsManager.instance.objectInfoUI;
        }

    }
}