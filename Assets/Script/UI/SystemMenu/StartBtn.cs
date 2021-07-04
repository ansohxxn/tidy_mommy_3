using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtn : MonoBehaviour
{
    public void Load_MainScene()
    {
        Time.timeScale = 1f;

        Managers.Audio.Play_SFX(Define.SFX.Click);
        
        Managers.Scene.LoadScene(Define.Scene.Main);
    }

    public void Load_StartScene()
    {
        Time.timeScale = 1f;

        Managers.Audio.Play_SFX(Define.SFX.Click);

        Managers.Scene.LoadScene(Define.Scene.Start);
    }
}
