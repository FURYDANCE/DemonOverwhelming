using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildAssetsByExcel : Editor
{
    /// <summary>
    /// 创建对话表的ScriptableObject对象
    /// </summary>
    [MenuItem("创建Asset/创建对话表")]
    public static void ExcuteBuild()
    {
        ExcelDataManager excel_manager = ScriptableObject.CreateInstance<ExcelDataManager>();
        excel_manager.plotsData = ExcelAccess.SelectMenuTable();
        string Path = "Assets/Resources/DataTables/PlotsDatas.asset";

        AssetDatabase.CreateAsset(excel_manager, Path);
        AssetDatabase.Refresh();

        Debug.Log("配置表文件制作成功");
    }
}
