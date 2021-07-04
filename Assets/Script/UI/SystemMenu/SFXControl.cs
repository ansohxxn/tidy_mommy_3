using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXControl : MonoBehaviour, ISoundControl
{
    public void SettingVolume(float volume)
    {
        Managers.Audio.SFX_Volume_Setting(volume);
    }

    public void On_and_Off_Sound(bool isOn)
    {
        if (isOn) Managers.Audio.canSFX = true;
        else Managers.Audio.canSFX = false;
    }
}
