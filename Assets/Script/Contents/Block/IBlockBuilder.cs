using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockBuilder
{
    void Block_OnEnable(Block block, Define.Block block_name);
}
