using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ӣ�����ݼ���", menuName = "ScriptableObject/Ӣ�����ݼ���", order = 0)]
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
