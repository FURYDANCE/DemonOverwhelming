using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffectOnStart : MonoBehaviour
{
    public AudioClip clip;
    private void Start()
    {
        SoundManager.instacne.PlayeSoundEffect(clip);
    }
}
