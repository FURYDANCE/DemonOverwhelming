using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR

public class BuildAssetsByExcel : Editor
{
    /// <summary>
    /// �����Ի����ScriptableObject����
    /// </summary>
    [MenuItem("����Asset/�������ݱ�")]
    public static void ExcuteBuild()
    {
        ExcelDataManager excel_manager = ScriptableObject.CreateInstance<ExcelDataManager>();
        excel_manager.plotsData = ExcelAccess.SelectMenuTable();
        ExcelAccess.SelectEntityTable(out excel_manager.unitDatas, out excel_manager.characterDatas,
            out excel_manager.cardDatas, out excel_manager.missileDatas, out excel_manager.damageDatas,out excel_manager.skillDatas);
        string Path = "Assets/AddressableAssetsData/Data/PlotsDatas.asset";
        AssetDatabase.CreateAsset(excel_manager, Path);
        AssetDatabase.Refresh();

        //JsonEditor.WritingJson(Application.persistentDataPath, JsonEditor.unitFileName, excel_manager.unitDatas);
        //JsonEditor.WritingJson(Application.persistentDataPath, JsonEditor.cardFileName, excel_manager.cardDatas);
        //JsonEditor.WritingJson(Application.persistentDataPath, JsonEditor.missileFileName, excel_manager.missileDatas);
        Debug.Log("���ñ��ļ������ɹ�");
    }
    [MenuItem("����Asset/�������ݱ��ļ���json")]
    public static void SaveBuildToJson()
    {


        //ExcelDataManager excel_manager = Resources.Load<ExcelDataManager>("DataTables/PlotsDatas") as ExcelDataManager;
        //JsonEditor.WritingJson(Application.persistentDataPath, JsonEditor.unitFileName, excel_manager.unitDatas);
        //JsonEditor.WritingJson(Application.persistentDataPath, JsonEditor.cardFileName, excel_manager.cardDatas);
        //JsonEditor.WritingJson(Application.persistentDataPath, JsonEditor.missileFileName, excel_manager.missileDatas);

        //Debug.Log("���ñ��ļ������ɹ�");
    }

}
#endif