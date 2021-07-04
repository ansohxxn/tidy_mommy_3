using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BombCommand : BasicCommand
{
    public override bool CanRemove(int col, int row)
    {
        return true;
    }

    public override void Clear(int col, int row)
    {
        Clear_One_Block(col, row);

        for (int i = row - 1, count = 0; i >= 0 && count < 2 && board.blocks[col].Count > i; ++count, --i)
        {
            if (board.blocks[col][i].blockdata.blockType == Define.BlockType.Color)
                Clear_One_Block(col, i);
        }

        if (col == 1 || col == 2)
        {
            if (board.blocks[col - 1].Count > row)
                Clear_One_Block(col - 1, row);

            for (int i = row - 1, count = 0; i >= 0 && count < 2 && board.blocks[col - 1].Count > i; ++count, --i)
            {
                if (board.blocks[col - 1][i].blockdata.blockType == Define.BlockType.Color)
                    Clear_One_Block(col - 1, i);
            }
        }

        if (col == 0 || col == 1)
        {
            if (board.blocks[col + 1].Count > row)
                Clear_One_Block(col + 1, row);

            for (int i = row - 1, count = 0; i >= 0 && count < 2 && board.blocks[col + 1].Count > i; ++count, --i)
            {
                if (board.blocks[col + 1][i].blockdata.blockType == Define.BlockType.Color)
                    Clear_One_Block(col + 1, i);
            }
        }

        Clear_Additional_Blocks();
        board.Show_Bomb_Seconds_Img(board.pos.blockPos[col, row].position);
    }
}
