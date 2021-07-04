using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : BaseScene
{
    protected override void Init()
    {
        SceneType = Define.Scene.Main;

        Managers.Game.Init();
        Managers.Pool.Init();

        Managers.Audio.Play_BGM();
        Managers.Audio.Pitch_Setting(Define.GameState.Normal);
    }

    public override void Clear()
    {
        Managers.Pool.Clear();
        Managers.Audio.Stop_BGM();
    }
}
