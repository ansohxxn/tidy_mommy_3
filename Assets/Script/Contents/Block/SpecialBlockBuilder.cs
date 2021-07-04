using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBlcokBuilder : IBlockBuilder
{
    public void Block_OnEnable(Block block, Define.Block block_name)
    {
        block.blockdata = Managers.Resource.Get_Block_Data(block_name);
        block.myGameObject.name = block_name.ToString();

        block.mySpriteRenderer.sprite = Managers.Resource.Get_Special_Block_Sprite(block_name);
    }
}
