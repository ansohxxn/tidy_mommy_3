using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControl : MonoBehaviour, ISoundControl
{
    public void SettingVolume(float volume)
    {
        Managers.Audio.sfxClick_audioSource.volume = volume;
        Managers.Audio.sfxMove_audioSource.volume = volume;
        Managers.Audio.sfxSuccess_audioSource.volume = volume;
    }

    public void On_and_Off_Sound(bool isOn)
    {
        if (isOn) Managers.Audio.canSFX = true;
        else Managers.Audio.canSFX = false;
    }
}
