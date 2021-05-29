using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string bestScore_key = "BestScore";
    public int bestScore = 0;

    public void Init()
    {
        if (PlayerPrefs.HasKey(bestScore_key))
            bestScore = PlayerPrefs.GetInt(bestScore_key);
    }

    public void UpdateBestScore(int _num)
    {
        PlayerPrefs.SetInt(bestScore_key, _num);
    }
}
