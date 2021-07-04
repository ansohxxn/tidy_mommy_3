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
        if (Managers.Game.isGameOver) 
            return;

        if (Managers.Game.prevClickedCol == Define.Column.None) // 클릭 상태였던 블록이 없다면 selectedFrame 활성화
        {
            int col = (int)colID;
            if (board.IsBlockEmpty(col)) 
                return;
            Managers.Game.prevClickedCol = colID;

            int row = board.Get_Last_Row_Block_Index((int)colID);
            if (row >= Define.MAX_ROW_NUM) 
                return;

            Managers.Game.selectedFrame.Setting(board.GetLastRowBlock((int)colID), pos.blockPos[col,row]);
        }
        else if (Managers.Game.prevClickedCol == colID)  // 클릭 했던 열 동일하게 또 누르면 selectedFrame 해제 
        {
            Managers.Game.prevClickedCol = Define.Column.None;
            Set_Basic_State_Sprite();
        }
        else
        {
            board.Move_And_Remove(Managers.Game.selectedFrame.selectedBlock, (int)Managers.Game.prevClickedCol, (int)colID);
            Set_Basic_State_Sprite();
        }
    }

    private void Set_Basic_State_Sprite()
    {
        if (Managers.Game.selectedFrame.selectedBlock == null)
            return;

        if (Managers.Game.selectedFrame.selectedBlock.blockdata.blockType == Define.BlockType.Color)
        {
            ColorBlockBuilder colorBlockBuilder = Managers.Game.selectedFrame.selectedBlock.blockBuilder as ColorBlockBuilder;
            if (colorBlockBuilder != null) colorBlockBuilder.Set_State_Sprite(Managers.Game.selectedFrame.selectedBlock, Define.ClickState.NotClicked);
        }
        Managers.Game.selectedFrame.SetDisactive();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
