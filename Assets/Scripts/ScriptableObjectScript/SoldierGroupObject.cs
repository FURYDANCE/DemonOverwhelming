using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "�������ݼ���", menuName = "ScriptableObject/�������ݼ���", order = 0)]
public class SoldierGroupObject : ScriptableObject
{
    public List<SoldierGroup_> group;
}
[System.Serializable]
public class SoldierGroup_
{
    public string id;
    public GameObject contentObject;
}

