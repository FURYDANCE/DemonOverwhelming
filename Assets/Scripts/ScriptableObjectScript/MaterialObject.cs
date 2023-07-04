using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Matrial���ݼ���", menuName = "ScriptableObject/Matrial���ݼ���", order = 0)]
public class MaterialObject : ScriptableObject
{
    public Material defaultMaterial;
    public Material soldierMaterial;
    public Material onMouseCoverMaterial;
    public Material onSelectedMaterial;
}