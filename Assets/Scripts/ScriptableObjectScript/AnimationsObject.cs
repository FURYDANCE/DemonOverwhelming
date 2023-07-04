using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "动画数据集合", menuName = "ScriptableObject/动画数据集合", order = 0)]
public class AnimationsObject : ScriptableObject
{
    public List<AnimationsList> animations;
}