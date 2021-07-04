using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Block
{
    public Define.BlockType Get_Randomly_ColorBlock_Or_SpecialBlock() // 색깔 블록 or 스페셜 블록 뽑기 (확률이 다르다) 
    {
        float res = Random.Range(0f, 1f);
        if (res < Define.SPECIAL_BLOCK_PROBABILITY)
            return Define.BlockType.Special;
        else
            return Define.BlockType.Color;
    }
}

public class Pick_Color_Block : Pick_Block
{
    List<int> randomBox = new List<int>();

    public int Get_Random_Block(List<List<Block>> blocks, int col)  // 색깔 블록 랜덤 뽑기. except 블록은 제외하고 뽑는다. 현재 레벨 내의 블록들만 뽑힐 수 있다.  
    {
        randomBox.Clear();

        int max = Managers.Game.level;
        if (Managers.Game.gameState == Define.GameState.SuperFever)
            max = (int)Define.Block.Blue;

        int except = Get_Block_To_Be_Excluded(blocks, col);
        for (int i = 0; i <= max; ++i)
            if (i != except)
                randomBox.Add(i);

        int res = randomBox[Random.Range(0, randomBox.Count)];
        return res;
    }

    private int Get_Block_To_Be_Excluded(List<List<Block>> blocks, int col)
    {
        int except = -1;
        if (blocks[col].Count >= 2)
        {
            if (blocks[col][0].blockdata.blockType == Define.BlockType.Color &&
                blocks[col][1].blockdata.blockType == Define.BlockType.Color &&
                blocks[col][0].blockdata.block_name == blocks[col][1].blockdata.block_name)
                except = (int)blocks[col][0].blockdata.block_name;
        }
        return except;
    }
}

public class Pick_Special_Block : Pick_Block
{
    List<int> randomBox = new List<int>();

    public int Get_Random_Block()
    {
        randomBox.Clear();

        int start = Define.NUM_OF_COLOR_BLOCK;
        for (int i = start; i < Define.NUM_OF_COLOR_BLOCK + Define.NUM_OF_SPECIAL_BLOCK; ++i)
            if (i != (int)Define.Block.Bomb)
                randomBox.Add(i);

        int res = randomBox[Random.Range(0, randomBox.Count)];
        return res;
    }
}
