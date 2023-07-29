using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("�������ݼ���")]
    public MusicObject musics;
    [Header("��Ч���ݼ���")]
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
    public void PlayeSoundEffect(AudioClip soundEffect)
    {
        soundEffectSource.PlayOneShot(soundEffect);
    }

}
