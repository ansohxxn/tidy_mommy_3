using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeBarInput : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Board board;
    [SerializeField] ScoreText score_text;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Managers.Game.isGameOver) return;
        Managers.Audio.Play_SFX(Define.SFX.Click);
        SetScore();
        board.Make_Next_Rows();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        
    }

    private void SetScore()
    {
        Managers.Game.total_score += Define.TIMEBAR_TOUCH_SCORE;
        score_text.Text_UI_Update();
    }
}
