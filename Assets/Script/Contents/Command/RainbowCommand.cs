using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RainbowCommand : BasicCommand
{
    Block attached_block;
    public override void Clear(int col, int row)
    {
        Clear_One_Block(col, row);

        for (int i = 0; i < Define.MAX_COL_NUM; ++i)
        {
            int size = board.blocks[i].Count;
            for (int j = size - 1; j >= 0; --j)
            {
                if (board.blocks[i][j].blockdata.blockType == Define.BlockType.Color && attached_block.blockdata.block_name == board.blocks[i][j].blockdata.block_name)
                    Clear_One_Block(i, j);
            }
        }

        Clear_Additional_Blocks();
    }

    public override bool CanRemove(int col, int row)
    {
        attached_block = board.blocks[col][row - 1];
        if (attached_block.blockdata.blockType == Define.BlockType.Special)
            return false;
        return true;
    }
}