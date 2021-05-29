using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager 
{ 
    public AudioSource bgm_audioSource;
    public AudioSource sfxMove_audioSource;
    public AudioSource sfxSuccess_audioSource;
    public AudioSource sfxClick_audioSource;
    public AudioSource sfx_audioSource;

    public bool canSFX = true;

    public float default_volume = 0.5f;

    public void Init()
    {
        // bgm
        AudioSource_Setting(bgm_audioSource, default_volume, true, true, Managers.Resource.GetBGM());

        // sfx
        AudioSource_Setting(sfxMove_audioSource, default_volume, false, false, Managers.Resource.GetSFX_Move());
        AudioSource_Setting(sfxSuccess_audioSource, default_volume, false, false, Managers.Resource.GetSFX_Success());
        AudioSource_Setting(sfxClick_audioSource, default_volume, false, false, Managers.Resource.GetSFX_Click());
        AudioSource_Setting(sfx_audioSource, default_volume, false, false);
    }

    public void AudioSource_Setting(AudioSource _audio, float _volume, bool _loop, bool _playOnAwake, AudioClip _clip = null)
    {
        _audio.clip = _clip;
        _audio.volume = _volume;
        _audio.loop = _loop;
        _audio.playOnAwake = _playOnAwake;
    }
}
