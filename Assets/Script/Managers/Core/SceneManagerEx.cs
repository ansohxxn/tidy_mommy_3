using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene scene)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(scene));
    }

    public AsyncOperation LoadSceneAsync(Define.Scene scene)
    {
        CurrentScene.Clear();
        return SceneManager.LoadSceneAsync(GetSceneName(scene));
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name;
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
