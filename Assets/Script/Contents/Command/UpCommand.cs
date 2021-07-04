using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpCommand : BasicCommand
{
    public override bool CanRemove(int col, int row)
    {
        return true;
    }

    public override void Clear(int col, int row)
    {
        Clear_One_Block(col, row);
        int size = board.blocks[col].Count;
        for (int i = size - 1; i >= 0; --i)
        {
            if (board.blocks[col][i].blockdata.blockType == Define.BlockType.Color)
                Clear_One_Block(col, i);
        }
    }
}

