using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "英雄数据集合", menuName = "ScriptableObject/英雄数据集合", order = 0)]
public class HeroDataObject : ScriptableObject
{
    public List<HeroData> heroDatas; 
}
[System.Serializable]
public class HeroData
{
    public string id;
    public GameObject hero;
}
