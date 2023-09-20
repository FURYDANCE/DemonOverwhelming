using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DemonOverwhelming
{

    /// <summary>
    /// ���һ����ť֮����ʾ��Ӧ��UI������������UI
    /// </summary>
    public class UI_SelectGroup : MonoBehaviour
    {
        public Button[] btns;
        public GameObject[] uis;
        void Start()
        {
            SelectBtn(0);
        }
        public void SelectBtn(int index)
        {
            foreach (Button btn in btns)
            {
                btn.GetComponent<Image>().color = Color.white;
            }
            btns[index].GetComponent<Image>().color = Color.red;
            foreach (GameObject go in uis)
            {
                go.SetActive(false);
            }
            uis[index].SetActive(true);
        }
    }
}