using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
[System.Serializable]
public class AnimationsList
{
    public string id;
    public AnimatorController animatorController;
    public Motion animation_Idle;
    public Motion animation_Attack;
    public Motion animation_Walk;
    public Motion animation_Die;

}