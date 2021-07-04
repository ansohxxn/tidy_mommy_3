using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMControl : MonoBehaviour, ISoundControl
{
    public void SettingVolume(float volume)
    {
        Managers.Audio.BGM_Volume_Setting(volume);
    }

    public void On_and_Off_Sound(bool isOn)
    {
        if (isOn) Managers.Audio.Play_BGM();
        else Managers.Audio.Stop_BGM();
    }
}
