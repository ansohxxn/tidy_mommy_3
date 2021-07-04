using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    #region managers
    PoolManager _pool = new PoolManager();
    public static PoolManager Pool { get { return Instance._pool; } }

    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }

    CoroutineManager _co = new CoroutineManager();
    public static CoroutineManager Co { get { return Instance._co; } }

    ResourceManager _resource = new ResourceManager();
    public static ResourceManager Resource { get { return Instance._resource; } }

    AudioManager _audio = new AudioManager();
    public static AudioManager Audio { get { return Instance._audio; } }

    SceneManagerEx _scene = new SceneManagerEx();
    public static SceneManagerEx Scene { get { return Instance._scene; } }

    DataManager _data = new DataManager();
    public static DataManager Data { get { return Instance._data; } }

    #endregion

    void Start()
    {
        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true);
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
                s_instance = go.GetComponent<Managers>();
            }

            DontDestroyOnLoad(go);

            Generate_AudioSource();

            s_instance._data.Init();
            s_instance._resource.Init();
            s_instance._audio.Init();
        }
    }

    public static void Clear()
    {
        
    }

    static void Generate_AudioSource()
    {
        s_instance._audio.bgmAudioSources = s_instance.gameObject.AddComponent<AudioSource>();
        foreach (Define.SFX sfx in Enum.GetValues(typeof(Define.SFX)))
            s_instance._audio.sfxAudioSources[sfx] = s_instance.gameObject.AddComponent<AudioSource>();
    }
}
