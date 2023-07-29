using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[System.Serializable]
public class AnimationsList
{
    public string id;
    //public string animationContollerName;
    public RuntimeAnimatorController animatorController;
    public Motion animation_Idle;
    public Motion animation_Attack;
    public Motion animation_Walk;
    public Motion animation_Die;

}