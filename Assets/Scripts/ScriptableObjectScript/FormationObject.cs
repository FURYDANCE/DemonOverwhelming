using System.Collections;
using System.Collections.Generic;
using DemonOverwhelming;
using UnityEngine;
[CreateAssetMenu(fileName = "阵型数据集合", menuName = "ScriptableObject/阵型数据集合", order = 0)]
public class FormationObject : ScriptableObject
{
    public List<Formation> formations;
}
/// <summary>
/// 阵型的数据类，存放id和阵型类，可以通过id找到对应类中的数据
/// </summary>
[System.Serializable]
public class Formation
{
    public string id;
    public SoldierFormation formation;
}
