using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBtn : MonoBehaviour
{
    public void InitBestScore()
    {
        if (Managers.Audio.canSFX) Managers.Audio.sfxClick_audioSource.Play();
        Managers.Data.UpdateBestScore(0);
    }
}
