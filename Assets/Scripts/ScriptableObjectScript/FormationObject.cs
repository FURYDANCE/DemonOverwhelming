using System.Collections;
using System.Collections.Generic;
using DemonOverwhelming;
using UnityEngine;
[CreateAssetMenu(fileName = "阵型数据集合", menuName = "ScriptableObject/阵型数据集合", order = 0)]
public class FormationObject : ScriptableObject
{
    public List<Formation> formations;
}
[System.Serializable]
public class Formation
{
    public string id;
    public SoliderGroup contentFormation;
}
