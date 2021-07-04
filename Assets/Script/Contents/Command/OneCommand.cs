using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OneCommand : BasicCommand
{
    public override void Clear(int col, int row)
    {
        Clear_One_Block(col, row);
        Clear_One_Block(col, row - 1);
    }

    public override bool CanRemove(int col, int row)
    {
        Block attached_block = board.blocks[col][row - 1];
        if (attached_block.blockdata.blockType == Define.BlockType.Special)
            return false;
        return true;
    }
}
