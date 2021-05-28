using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager 
{ 
    public AudioSource bgm_audioSource;
    public AudioSource sfxMove_audioSource;
    public AudioSource sfxSuccess_audioSource;
    public AudioSource sfxClick_audioSource;

    public bool canSFX = true;

    private float default_volume = 0.5f;

    public void Init()
    {
        // bgm
        AudioSource_Setting(bgm_audioSource, Managers.Resource.GetBGM(), default_volume, true, true);
        bgm_audioSource.Play();

        // sfx
        AudioSource_Setting(sfxMove_audioSource, Managers.Resource.GetSFX_Move(), default_volume, false, false);
        AudioSource_Setting(sfxSuccess_audioSource, Managers.Resource.GetSFX_Success(), default_volume, false, false);
        AudioSource_Setting(sfxClick_audioSource, Managers.Resource.GetSFX_Click(), default_volume, false, false);
    }

    private void AudioSource_Setting(AudioSource _audio, AudioClip _clip, float _volume, bool _loop, bool _playOnAwake)
    {
        _audio.clip = _clip;
        _audio.volume = _volume;
        _audio.loop = _loop;
        _audio.playOnAwake = _playOnAwake;
    }
}
