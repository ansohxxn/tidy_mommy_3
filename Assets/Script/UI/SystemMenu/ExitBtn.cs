using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBtn : MonoBehaviour
{
    public void ExitGame()
    {
        Managers.Audio.Play_SFX(Define.SFX.End);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
