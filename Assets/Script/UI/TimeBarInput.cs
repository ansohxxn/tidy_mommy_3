using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimeBarInput : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    const int timebar_touch_score = 5000;
    [SerializeField] Board board;
    [SerializeField] ScoreText score_text;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Managers.Game.isGameOver) return;
        SetScore();
        board.MakeNextRows();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        
    }

    private void SetScore()
    {
        Managers.Game.score += timebar_touch_score;
        score_text.Text_UI_Update();
    }
}
