using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpineԤ�Ƽ����ݼ���", menuName = "ScriptableObject/SpineԤ�Ƽ����ݼ���", order = 0)]
public class SpinePrefabObject : ScriptableObject
{
    public List<SpinePrefab> spinePrefabs;
}
[System.Serializable]
public class SpinePrefab
{
    [Header("id")]
     public string id;
    [Header("����")]
    public string m_name;
    [Header("��Ӧ��Ԥ�Ƽ�")]
    public GameObject spinePrefab;
}