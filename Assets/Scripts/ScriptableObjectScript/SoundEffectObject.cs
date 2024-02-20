using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "音效数据集合", menuName = "ScriptableObject/音效数据集合", order = 0)]

public class SoundEffectObject : ScriptableObject
{
    public List<SoundEffectData> soundEffects;

}
[System.Serializable]
public class SoundEffectData
{
    public string name;
    public string id;
    public AudioClip clip;
}
