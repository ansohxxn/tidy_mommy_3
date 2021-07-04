using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBtn : MonoBehaviour
{
    public void InitBestScore()
    {
        Managers.Audio.Play_SFX(Define.SFX.Click);
        Managers.Data.UpdateBestScore(0);
    }
}
