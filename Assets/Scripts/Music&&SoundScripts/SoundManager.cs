using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DemonOverwhelming
{

    public class SoundManager : MonoBehaviour
    {
        [Header("音乐数据集合")]
        public MusicObject musics;
        [Header("音效数据集合")]
        public SoundEffectObject soundEffects;
        public static SoundManager instacne;
        public AudioSource musicSource;
        public AudioSource soundEffectSource;
        private void Awake()
        {
            if (instacne != null)
                Destroy(gameObject);
            instacne = this;
            DontDestroyOnLoad(this);
        }
        public void ChangeMusic(AudioClip newMusic)
        {
            musicSource.Stop();
            musicSource.clip = newMusic;
            musicSource.Play();
        }
        /// <summary>
        /// 播放音效，传入audioclip
        /// </summary>
        /// <param name="soundEffect"></param>
        public void PlaySoundEffect(AudioClip soundEffect)
        {
            if (soundEffect == null)
            {
                Debug.LogWarning("没有成功传入音效");
                return;
            }
            soundEffectSource.PlayOneShot(soundEffect);
        }
        /// <summary>
        /// 播放音效，传入对应音效的id或名称
        /// </summary>
        /// <param name="id_or_name"></param>
        public void PlaySoundEffect(string id_or_name)
        {
            PlaySoundEffect(GetAudioClipByIdOrName(id_or_name));
        }
        /// <summary>
        /// 通过id或名称找到对应的音效
        /// </summary>
        /// <param name="id_or_name"></param>
        /// <returns></returns>
        public AudioClip GetAudioClipByIdOrName(string id_or_name)
        {
            if (id_or_name == "")
                return null;
            AudioClip a;
            if (soundEffects.soundEffects.Find((SoundEffectData vd) => vd.id == id_or_name) != null)
                a = soundEffects.soundEffects.Find((SoundEffectData vd) => vd.id == id_or_name).clip;
            else
                a = soundEffects.soundEffects.Find((SoundEffectData vd) => vd.name == id_or_name).clip;
            return a;
        }
    }
}