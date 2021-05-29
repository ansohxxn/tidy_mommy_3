using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverText : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Set_GameOver_Text()
    {
        gameObject.SetActive(true);
    }
}
