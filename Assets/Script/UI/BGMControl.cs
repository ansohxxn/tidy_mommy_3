﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMControl : MonoBehaviour, ISoundControl
{
    public void SettingVolume(float volume)
    {
        Managers.Audio.bgm_audioSource.volume = volume;
    }

    public void On_and_Off_Sound(bool isOn)
    {
        if (isOn) Managers.Audio.bgm_audioSource.Play();
        else Managers.Audio.bgm_audioSource.Stop();
    }
}
