using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColorCommand : BasicCommand
{
    public override void Clear(int col, int row)
    {
        for (int i = 0; i < Define.MAX_COL_NUM; ++i)
            Clear_One_Block(col, row - i);
    } 

    public override bool CanRemove(int col, int row)
    {
        if (board.blocks[col].Count < Define.CAN_REMOVE_NUM)
            return false;
        if (board.blocks[col][row - 1].blockdata.blockType != Define.BlockType.Color || board.blocks[col][row - 2].blockdata.blockType != Define.BlockType.Color)
            return false;
        if (board.blocks[col][row - 1].blockdata.block_name != board.blocks[col][row].blockdata.block_name ||
            board.blocks[col][row - 2].blockdata.block_name != board.blocks[col][row].blockdata.block_name)
            return false;

        return true;
    }
}