using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "��Ч���ݼ���", menuName = "ScriptableObject/��Ч���ݼ���", order = 0)]

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
