using UnityEngine;
using DemonOverwhelming;
[System.Serializable]
public class PlotsData
{
    public int id;                          //�Ի�id
    public string content_cn;               //�Ի�����
    public string speakerNmae;              //˵��������
    public bool isLast;                     //�Ƿ�Ϊ����
    public Sprite left_stand;               //������
    public Sprite right_stand;              //������
    public string event_1;                  //�Ի��¼�
    public string event_2;
    public string event_3;
    public string event_4;
    public bool haveOption;                 //�Ƿ���ѡ��
    public string optionContent_1;          //ѡ��1����
    public string optionContent_2;          //ѡ��2����
    public int option_1_targetId;           //ѡ��1��תid
    public int option_2_targetId;           //ѡ��2��תid
}
