using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverText : MonoBehaviour
{
    private TextMeshProUGUI game_over;

    void Start()
    {
        game_over = GetComponent<TextMeshProUGUI>();
        game_over.text = "Game Over";
        game_over.enabled = false;
    }

    public void Set_GameOver_Text()
    {
        game_over.enabled = true;
    }
}
