using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager 
{ 
    public AudioSource audioSource;
    AudioClip audioClip;

    public void Init()
    {
        audioClip = Managers.Resource.GetBGM();

        audioSource.clip = audioClip;
        audioSource.volume = 0.2f;
        audioSource.loop = true;

        audioSource.Play();
    }
}
