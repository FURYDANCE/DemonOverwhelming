using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Matrial数据集合", menuName = "ScriptableObject/Matrial数据集合", order = 0)]
public class MaterialObject : ScriptableObject
{
    public Material defaultMaterial;
    public Material soldierMaterial;
    public Material onMouseCoverMaterial;
    public Material onSelectedMaterial;
}