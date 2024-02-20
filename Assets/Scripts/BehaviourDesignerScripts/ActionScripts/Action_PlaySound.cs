using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
namespace DemonOverwhelming
{
    public class Action_PlaySound : Action
    {
        public SharedEntity thisEntity;
        Entity entity;
        [Header("����id����Ҫ���ŵ���Ч")]
        public string id_or_name;
        public AudioClip targetAudioClip;
        [Header("��������ʵ��������Ч�����")]
        public bool playAttackAudio;
        public bool playOnHitAudio;
        public bool playOnDieAudio;
        public override void OnStart()
        {
            entity = thisEntity.Value;
            if (id_or_name != "")
                SoundManager.instacne.PlaySoundEffect(id_or_name);
            if (targetAudioClip != null)
                SoundManager.instacne.PlaySoundEffect(targetAudioClip);
            if (playAttackAudio)
                SoundManager.instacne.PlaySoundEffect(entity.attackAudioId);
            if (playOnHitAudio)
                SoundManager.instacne.PlaySoundEffect(entity.onHitAudioId); 
            if (playOnDieAudio)
                SoundManager.instacne.PlaySoundEffect(entity.onDieAudioId);
        }
        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}