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

    void Awake()
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
            }
            else return;

            s_instance = go.GetComponent<Managers>();
            s_instance._audio.audioSource = go.AddComponent<AudioSource>();

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
}
