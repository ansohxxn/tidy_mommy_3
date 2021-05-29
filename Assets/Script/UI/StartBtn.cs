using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    public void Load_MainScene()
    {
        Time.timeScale = 1f;
        if (Managers.Audio.canSFX) Managers.Audio.sfxClick_audioSource.Play();
        
        Managers.Scene.LoadScene(Define.Scene.Main);
    }

    public void Load_StartScene()
    {
        Time.timeScale = 1f;
        if (Managers.Audio.canSFX) Managers.Audio.sfxClick_audioSource.Play();
        
        Managers.Scene.LoadScene(Define.Scene.Start);
    }
}
