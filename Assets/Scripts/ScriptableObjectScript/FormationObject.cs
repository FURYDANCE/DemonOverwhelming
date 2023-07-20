using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "�������ݼ���", menuName = "ScriptableObject/�������ݼ���", order = 0)]
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
