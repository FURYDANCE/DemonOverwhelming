using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildAssetsByExcel : Editor
{
    /// <summary>
    /// �����Ի����ScriptableObject����
    /// </summary>
    [MenuItem("����Asset/�����Ի���")]
    public static void ExcuteBuild()
    {
        ExcelDataManager excel_manager = ScriptableObject.CreateInstance<ExcelDataManager>();
        excel_manager.plotsData = ExcelAccess.SelectMenuTable();
        string Path = "Assets/Resources/DataTables/PlotsDatas.asset";

        AssetDatabase.CreateAsset(excel_manager, Path);
        AssetDatabase.Refresh();

        Debug.Log("���ñ��ļ������ɹ�");
    }
}
