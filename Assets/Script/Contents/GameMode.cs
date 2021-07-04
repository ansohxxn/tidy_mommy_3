using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    private int feverCombo = 0;
    private Coroutine co;

    [SerializeField] ComboText comboText;
    [SerializeField] StateText stateText;
    [SerializeField] SpriteRenderer char_spriteRenderer;
    [SerializeField] SpriteRenderer background_spriteRenderer;
    Board board;

    void Start()
    {
        board = GetComponent<Board>();
    }

    public void Update_Combo(int moveCount, Vector2 pos)
    {
        if (moveCount <= Managers.Game.level + 1)
        {
            if (++Managers.Game.combo >= 1)
            {
                Update_ComboText(pos);
                Update_Mode();
            }
        }
        else
        {
            Managers.Game.combo = -1;
            Update_Mode();
        }
    }

    private void Update_Mode()
    {
        switch (Managers.Game.gameState)
        {
            case Define.GameState.Normal:
                if (Managers.Game.combo >= Define.COMBO_TO_FEVER_MODE)
                {
                    // Normal -> Fever
                    Setting(Define.GameState.Fever);
                    Show_StateText(Define.GameState.Fever);
                }
                break;

            case Define.GameState.Fever:
                ++feverCombo;
                if (Managers.Game.combo == -1)
                {
                    // Fever -> Normal
                    Setting(Define.GameState.Normal);
                    feverCombo = 0;
                }
                if (feverCombo >= Define.COMBO_TO_SUPER_FEVER_MODE)
                {
                    // Fever -> SuperFever
                    Setting(Define.GameState.SuperFever);
                    co = StartCoroutine(SuperFever_Timer());
                    feverCombo = 0;

                    board.Clear_Init_Board();
                    board.Board_Init();

                    Show_StateText(Define.GameState.SuperFever);
                }
                break;

            case Define.GameState.SuperFever:
                // SuperFever -> Fever (10초 뒤)
                
                if (Managers.Game.combo == -1)
                {
                    // SuperFever -> Normal
                    StopCoroutine(co);
                    Setting(Define.GameState.Normal);
                }
                break;
        }
    }

    private void Setting(Define.GameState gameState)
    {
        Managers.Game.gameState = gameState;
        
        char_spriteRenderer.sprite = Managers.Resource.Get_Char_Sprite(gameState);
        background_spriteRenderer.sprite = Managers.Resource.Get_Background_Sprite(gameState);

        Managers.Audio.Pitch_Setting(gameState);
    }

    IEnumerator SuperFever_Timer()
    {
        yield return Managers.Co.WaitSeconds(Define.SUPER_FEVER_TIME);
        Setting(Define.GameState.Fever);
    }
    
    private void Update_ComboText(Vector2 pos)
    {
        comboText.myGameObject.SetActive(true);
        comboText.Show_ComboText(pos);
    }

    private void Show_StateText(Define.GameState gameState)
    {
        stateText.myGameObject.SetActive(true);
        stateText.Show_StateText(gameState);
    }
}
