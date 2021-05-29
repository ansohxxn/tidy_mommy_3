using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Start;

    void Start()
    {
        Init();
    }

    protected abstract void Init();
    public abstract void Clear();
}
