using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace DemonOverwhelming
{

    /// <summary>
    /// �����ͣ�ڼ���ͼ��������ʾ��Ϣ�Ľű�
    /// </summary>
    public class MouseHover_Skill : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public Skill skill;
        ObjectInfoUI infoUI;
        private void Start()
        {
            infoUI = SceneObjectsManager.instance.objectInfoUI;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("������");
            infoUI.DestoryAllText();
            SceneObjectsManager.instance.ShowObjectInfoUI(true);
            foreach (string s in skill.eachLevelDescriptions)
            {
                infoUI.CreateNewText(s);
                infoUI.SetTextColor(skill.skillLevel - 1);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("����˳�");

            SceneObjectsManager.instance.ShowObjectInfoUI(false);

        }
    }
}