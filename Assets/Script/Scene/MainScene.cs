﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Define.Scene.Main;

        Managers.Game.Init();
        Managers.Pool.Init();

        Managers.Audio.bgm_audioSource.Play();
    }

    public override void Clear()
    {
        Managers.Pool.Clear();
        Managers.Audio.bgm_audioSource.Stop();
    }
}