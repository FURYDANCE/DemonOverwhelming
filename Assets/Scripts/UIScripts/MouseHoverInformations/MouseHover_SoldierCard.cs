using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace DemonOverwhelming
{

    /// <summary>
    /// �����ͣ�ڱ��ֿ���ʱ��ʾ��Ϣ����Ľű�
    /// </summary>
    public class MouseHover_SoldierCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string[] descriptionAndStory;
        ObjectInfoUI infoUI;

        public void OnPointerEnter(PointerEventData eventData)
        {
            return;
            Debug.Log("������");
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

            Debug.Log("����˳�");

            SceneObjectsManager.instance.ShowObjectInfoUI(false);
        }

        private void Start()
        {
            infoUI = SceneObjectsManager.instance.objectInfoUI;
        }

    }
}