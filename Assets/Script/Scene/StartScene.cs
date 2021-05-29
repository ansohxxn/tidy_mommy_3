using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartScene : BaseScene
{
    [SerializeField] TextMeshProUGUI textMesh;

    protected override void Init()
    {
        SceneType = Define.Scene.Start;

        Managers.Data.Init();
        
        textMesh.text = string.Format("{0:#,##0}", Managers.Data.bestScore); 
    }

    public override void Clear()
    {
        
    }
}
