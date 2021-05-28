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
            s_instance = go.GetComponent<Managers>();

            Generate_AudioSource();

            s_instance._resource.Init();
            s_instance._pool.Init();
            s_instance._game.Init();
            s_instance._audio.Init();

            DontDestroyOnLoad(go);
        }
    }

    public static void Clear()
    {
        Pool.Clear();
    }

    static void Generate_AudioSource()
    {
        s_instance._audio.bgm_audioSource = s_instance.gameObject.AddComponent<AudioSource>();
        s_instance._audio.sfxMove_audioSource = s_instance.gameObject.AddComponent<AudioSource>();
        s_instance._audio.sfxSuccess_audioSource = s_instance.gameObject.AddComponent<AudioSource>();
        s_instance._audio.sfxClick_audioSource = s_instance.gameObject.AddComponent<AudioSource>();
    }
}
