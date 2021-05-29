using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMenu : MonoBehaviour
{
    public GameObject systemUI;

    void Start()
    {
        systemUI.SetActive(false);
    }

    public void Open_SystemMenu()
    {
        if (systemUI.activeSelf) return;
        if (Managers.Audio.canSFX) Managers.Audio.sfxClick_audioSource.Play();
        systemUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Close_SystemMenu()
    {
        if (!systemUI.activeSelf) return;
        if (Managers.Audio.canSFX) Managers.Audio.sfxClick_audioSource.Play();
        systemUI.SetActive(false);
        Time.timeScale = 1;
    }
}
