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
/// �༭json�浵���࣬�������ܽ���
/// </summary>
public class JsonEditor : MonoBehaviour
{
    //���ļ����ļ���
    public static string unitFileName = "unit";
    public static string cardFileName = "card";
    public static string missileFileName = "missile";
    //����·��
    public static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
    public static string streamingAssetsPath = Application.streamingAssetsPath;
    //public static string persistentDataPath = Application.persistentDataPath;
    // ���ܣ�ѡ��һЩ�������������ַ���ע�Ᵽ�ܣ�
    public static char[] keyChars = { 'z', 'l', 'z', 's', 'g' };
    #region Jsonת��
    /// <summary>
    /// �ӱ����ļ���ȡjson
    /// </summary>
    /// <param name="·����JsonEditor.��"></param>
    /// <param name="�ļ�����JsonEditor.��"></param>
    /// <param name="���ص�����"></param>
    public static List<T> ReadingJson<T> (string Path, string fileName, List<T> obj)
    {
        string myStr = null;
        //IO��ȡ
        myStr = GetMyJson(Path, fileName);
        //����
        myStr = Encryption(myStr);
        myStr= myStr.Remove(myStr.Length-2);
        Debug.Log(myStr);
        //ת��
        var jArray = JsonConvert.DeserializeObject<List<T>>(myStr);
        return jArray;
    }
    /// <summary>
    /// ���json
    /// </summary>
    /// <param name="Path"></param>
    public static void WritingJson(string Path, string fileName,object o)
    {
        //ת��json
        string json = JsonConvert.SerializeObject(o, new VectorConverter());
        //����
        json = Encryption(json);
        //���浽������ļ�
        SaveMyJson(Path, json, fileName);
    }
    #endregion
    #region IO��д
    /// <summary>
    ///     IO��ȡ����json
    /// </summary>
    /// <param name="desktopPath"></param>
    /// <returns></returns>
    private static string GetMyJson(string desktopPath, string fileName)
    {
        using (FileStream fsRead = new FileStream(string.Format("{0}\\" + fileName + ".json", desktopPath), FileMode.Open))
        {
            //��ȡ��ת��
            int fsLen = (int)fsRead.Length;
            byte[] heByte = new byte[fsLen];
            int r = fsRead.Read(heByte, 0, heByte.Length);
            return System.Text.Encoding.UTF8.GetString(heByte);
        }
    }

    /// <summary>
    /// ��json���浽����
    /// </summary>
    /// <param name="path"></param>
    /// <param name="json"></param>
    private static void SaveMyJson(string path, string json, string fileName)
    {
        using (FileStream fs = new FileStream(string.Format("{0}\\" + fileName + ".json", path), FileMode.Create))
        {
            //д��
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(json);
            }
        }
    }
    #endregion

    /// <summary>
    /// ���ַ�������/����
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
            // �ص㣺 ͨ�����õ��µ��ַ�
            char newChar = (char)(dataChar ^ keyChar);
            dataChars[i] = newChar;
            //Debug.Log(newChar);
        }

        return new string(dataChars);
    }
}


