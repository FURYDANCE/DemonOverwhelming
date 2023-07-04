using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 编辑json存档的类，包括加密解密
/// </summary>
public class JsonEditor : MonoBehaviour
{
    //各文件的文件名
    public static string unitFileName = "unit";
    public static string cardFileName = "card";
    public static string missileFileName = "missile";
    //创建路径
    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    public static string streamingAssetsPath = Application.streamingAssetsPath;
    //public static string persistentDataPath = Application.persistentDataPath;
    // 加密：选择一些用于亦或操作的字符（注意保密）
    public static char[] keyChars = { 'z', 'l', 'z', 's', 'g' };
    #region Json转换
    /// <summary>
    /// 从本地文件读取json
    /// </summary>
    /// <param name="路径（JsonEditor.）"></param>
    /// <param name="文件名（JsonEditor.）"></param>
    /// <param name="返回的类型"></param>
    public static List<T> ReadingJson<T> (string Path, string fileName, List<T> obj)
    {
        string myStr = null;
        //IO读取
        myStr = GetMyJson(Path, fileName);
        //解码
        myStr = Encryption(myStr);
        myStr= myStr.Remove(myStr.Length-2);
        Debug.Log(myStr);
        //转换
        var jArray = JsonConvert.DeserializeObject<List<T>>(myStr);
        return jArray;
    }
    /// <summary>
    /// 添加json
    /// </summary>
    /// <param name="Path"></param>
    public static void WritingJson(string Path, string fileName,object o)
    {
        //转成json
        string json = JsonConvert.SerializeObject(o, new VectorConverter());
        //加密
        json = Encryption(json);
        //保存到桌面的文件
        SaveMyJson(Path, json, fileName);
    }
    #endregion
    #region IO读写
    /// <summary>
    ///     IO读取本地json
    /// </summary>
    /// <param name="desktopPath"></param>
    /// <returns></returns>
    private static string GetMyJson(string desktopPath, string fileName)
    {
        using (FileStream fsRead = new FileStream(string.Format("{0}\\" + fileName + ".json", desktopPath), FileMode.Open))
        {
            //读取加转换
            int fsLen = (int)fsRead.Length;
            byte[] heByte = new byte[fsLen];
            int r = fsRead.Read(heByte, 0, heByte.Length);
            return System.Text.Encoding.UTF8.GetString(heByte);
        }
    }

    /// <summary>
    /// 将json保存到本地
    /// </summary>
    /// <param name="path"></param>
    /// <param name="json"></param>
    private static void SaveMyJson(string path, string json, string fileName)
    {
        using (FileStream fs = new FileStream(string.Format("{0}\\" + fileName + ".json", path), FileMode.Create))
        {
            //写入
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(json);
            }
        }
    }
    #endregion

    /// <summary>
    /// 将字符串加密/解密
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static string Encryption(string data)
    {
        char[] dataChars = data.ToCharArray();
        for (int i = 0; i < dataChars.Length; i++)
        {
            char dataChar = dataChars[i];
            char keyChar = keyChars[i % keyChars.Length];
            // 重点： 通过亦或得到新的字符
            char newChar = (char)(dataChar ^ keyChar);
            dataChars[i] = newChar;
            //Debug.Log(newChar);
        }

        return new string(dataChars);
    }
}


