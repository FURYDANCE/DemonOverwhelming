using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Excel;
using System.IO;
using System.Data;
public class ExcelAccess
{
    /// <summary>
    /// 对话表的文件名
    /// </summary>
    public static string PlotsExcel = "PlotsExcel";

    /// <summary>
    /// 读表，生成每句对话的对象，通过读表将对应的内容赋值给对象
    /// </summary>
    public static List<PlotsData> SelectMenuTable()
    {
        string excelName = PlotsExcel + ".xlsx";
        string sheetName = "sheet1";
        DataRowCollection collect = ReadExcel(excelName, sheetName);

        List<PlotsData> dataArray = new List<PlotsData>();
        for (int i = 3; i < collect.Count; i++)
        {
            if (collect[i][0].ToString() == "") continue; //行不是空的就开始执行
            PlotsData pd = new PlotsData
            {
                id = int.Parse(collect[i][0].ToString()),
                content_cn = collect[i][1].ToString(),
                isLast = collect[i][2].ToString() == "True" ? true : false,
                left_stand = collect[i][3].ToString(),
                right_stand = collect[i][4].ToString(),
                event_1 = collect[i][5].ToString(),
                event_2 = collect[i][6].ToString(),
                event_3 = collect[i][7].ToString(),
                event_4 = collect[i][8].ToString(),

                haveOption = collect[i][9].ToString() == "True" ? true : false,
                optionContent_1 = collect[i][10].ToString(),
                optionContent_2 = collect[i][11].ToString(),
                option_1_targetId = int.Parse(collect[i][12].ToString()),
                option_2_targetId = int.Parse(collect[i][13].ToString()),
            };
            dataArray.Add(pd);
        }
        return dataArray;
    }


    static DataRowCollection ReadExcel(string excelName, string sheetName)
    {
        string path = Application.dataPath + "/Resources/DataTables/" + excelName;
        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelReader.AsDataSet();
        return result.Tables[sheetName].Rows;

    }
}