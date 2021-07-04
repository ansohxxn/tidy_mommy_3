using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager 
{
    public AudioSource bgmAudioSources;
    public Dictionary<Define.SFX, AudioSource> sfxAudioSources = new Dictionary<Define.SFX, AudioSource>();

    public bool canSFX = true;

    public void Init()
    {
        // bgm
        BGM_Player_Setting();

        // sfx
        foreach (Define.SFX sfx in Enum.GetValues(typeof(Define.SFX)))
            SFX_Player_Setting(sfx);
    }

    private void BGM_Player_Setting()
    {
        bgmAudioSources.clip = Managers.Resource.bgm;
        bgmAudioSources.volume = Define.DEFAULT_VOLUME;
        bgmAudioSources.loop = true;
        bgmAudioSources.playOnAwake = true;
    }

    private void SFX_Player_Setting(Define.SFX sfx)
    {
        sfxAudioSources[sfx].clip = Managers.Resource.sfxAudioClips[sfx];
        sfxAudioSources[sfx].volume = Define.DEFAULT_VOLUME;
        sfxAudioSources[sfx].loop = false;
        sfxAudioSources[sfx].playOnAwake = false;
    }

    public void Play_BGM()
    {
        bgmAudioSources.Play();
    }

    public void Pitch_Setting(Define.GameState gameState)
    {
        bgmAudioSources.pitch = Define.BGM_SPEED[(int)gameState];
    }

    public void Play_SFX(Define.SFX sfx)
    {
        if (canSFX)
            sfxAudioSources[sfx].Play();
    }

    public void Stop_BGM()
    {
        bgmAudioSources.Stop();
    }

    public void BGM_Volume_Setting(float _volume)
    {
        bgmAudioSources.volume = _volume;
    }

    public void SFX_Volume_Setting(float _volume)
    {
        foreach (Define.SFX sfx in Enum.GetValues(typeof(Define.SFX)))
            sfxAudioSources[sfx].volume = _volume;
    }
}
