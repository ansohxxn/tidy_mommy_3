using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardInput : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Define.Column colID;
    [SerializeField] Board board;
    [SerializeField] Pos pos;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Managers.Game.isGameOver) return;

        if (Managers.Game.prevClickedCol == Define.Column.None) // 클릭 상태였던 블록이 없다면 selectedFrame 활성화
        {
            int col = (int)colID;
            if (board.IsBlockEmpty(col)) return;

            Managers.Game.prevClickedCol = colID;
            int row = board.GetLastRowBlock_Index((int)colID);

            if (row >= 10) return;
            Managers.Game.selectedFrame.Setting(board.GetLastRowBlock((int)colID), pos.blockPos[col,row]);
        }
        else if (Managers.Game.prevClickedCol == colID)  // 클릭 했던 열 동일하게 또 누르면 selectedFrame 해제 
        {
            Managers.Game.prevClickedCol = Define.Column.None;

            Managers.Game.selectedFrame.selectedBlock.SetBasicStateSprite();
            Managers.Game.selectedFrame.SetDisactive();
        }
        else
        {
            if (Managers.Game.selectedFrame.selectedBlock == null) return;
            board.MoveAndCheck(Managers.Game.selectedFrame.selectedBlock, (int)Managers.Game.prevClickedCol, (int)colID);

            Managers.Game.selectedFrame.selectedBlock.SetBasicStateSprite();
            Managers.Game.selectedFrame.SetDisactive();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
