using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager_MapScene : MonoBehaviour
{
    public GameObject levelInformationUI;
    [Header("�����ؿ���ϢUi�ĸ�����")]
    public Transform levelInfoParent;
    [Header("�Ƿ��д򿪵Ĺؿ���ϢUI")]
    public bool isInformationing;
    [Header("���ڴ򿪵Ĺؿ���ϢUI")]
    public GameObject nowLevelInformation;
    [Header("��ͼ��ק�ű�")]
    public MapDrag mapDrag;

    public static SceneManager_MapScene instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }
    /// <summary>
    /// ������ؿ�ͼ��֮��ִ�еķ���������ؿ���ϸ��Ϣ����
    /// </summary>
    /// <param name="information"></param>
    public void CreateLevelInformation(LevelBtn information)
    {
        if (isInformationing)
            return;
        mapDrag.SetCameraScale(15);
        mapDrag.RefreshPos(information.transform.position, new Vector2(10, -8));
        isInformationing = true;
        GameObject go = Instantiate(levelInformationUI, levelInfoParent);
        nowLevelInformation = go;
        go.name = "LEVEL INFO UI";
        Debug.Log("������UI");
        nowLevelInformation.GetComponent<LevelInformationUI>().SetInformation(information);
    }
    /// <summary>
    /// �����ȡ����ť��ִ�еķ����������ؿ���Ϣ����
    /// </summary>
    public void ReSelectLevel()
    {
        isInformationing = false;
        mapDrag.SetCameraScale(0);
        nowLevelInformation.GetComponent<Animator>().Play("LevelInfo_out");
        nowLevelInformation.AddComponent<LifeTime>().lifeTime = 3;
        return;
    }
}
