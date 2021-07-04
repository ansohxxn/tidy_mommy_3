using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SelectedFrame : MonoBehaviour
{
    [HideInInspector] public Transform myTransform; // caching
    [HideInInspector] public SpriteRenderer mySpriteRenderer;

    public Block selectedBlock;

    public void Init()
    {
        myTransform = GetComponent<Transform>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        SetDisactive();
    }

    public void SetActive()
    {
        if (mySpriteRenderer.sprite == null)
            mySpriteRenderer.sprite = Managers.Resource.Get_Selected_Block_Sprite();
        mySpriteRenderer.enabled = true;
    }

    public void SetDisactive()
    {
        mySpriteRenderer.enabled = false;
        selectedBlock = null;
    }

    public void Setting(Block block, Transform blockpos)
    {
        if (block.blockdata.blockType == Define.BlockType.Color)
        {
            ColorBlockBuilder colorBlockBuilder = block.blockBuilder as ColorBlockBuilder;
            if (colorBlockBuilder != null) colorBlockBuilder.Set_State_Sprite(block, Define.ClickState.Clicked);
        }
        selectedBlock = block;
        myTransform.position = blockpos.position;
        SetActive();
    }
}
