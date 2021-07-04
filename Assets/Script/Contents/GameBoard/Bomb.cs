using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb
{
    SpecialBlcokBuilder specialBlcokBuilder = new SpecialBlcokBuilder();

    public int Get_Index_To_Put_Bomb(List<List<Block>> blocks, Transform transform, ref bool canMakeBomb)  // 폭탄 아이템 놓을 col 랜덤하게 선택
    {
        if (canMakeBomb)
        {
            canMakeBomb = false;

            int bombCol = Random.Range(0, Define.MAX_COL_NUM);
            blocks[bombCol].Insert(0, Make_Bomb(transform));
            return bombCol;
        }
        else
            return -1;
    }

    private Block Make_Bomb(Transform transform) // 폭탄 아이템 생성
    {
        Block block = Managers.Game.Instantiate(transform);
        block.blockBuilder = specialBlcokBuilder;
        block.blockBuilder.Block_OnEnable(block, Define.Block.Bomb);

        return block;
    }
}
