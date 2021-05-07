using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [HideInInspector] public Block_Data blockdata;
    [HideInInspector] public Transform myTransform;
    [HideInInspector] public GameObject myGameObject;
    [HideInInspector] public SpriteRenderer mySpriteRenderer;

    public void Init()
    {
        myTransform = GetComponent<Transform>();
        myGameObject = gameObject;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ColorBlock_OnEnable(Define.ColorBlock block)
    {
        blockdata = Managers.Resource.GetColorBlockData(block);
        mySpriteRenderer.sprite = Managers.Resource.GetColorBlockSprite(block);

        myGameObject.name = block.ToString();
    }

    public void SpecialBlock_OnEnable(Define.SpecialBlock block)
    {
        blockdata = Managers.Resource.GetSpecialBlockData(block);
        mySpriteRenderer.sprite = Managers.Resource.GetSpecialBlockSprite(block);

        myGameObject.name = block.ToString();
    }

    public void SetBasicStateSprite() 
    {
        if (blockdata.blockType == Define.BlockType.Color) 
            mySpriteRenderer.sprite = Managers.Resource.GetColorBlockSprite(blockdata.colorBlock_name, Define.ClickState.NotClicked);
    }

    public void SetClickedStateSprite() 
    { 
        mySpriteRenderer.sprite = Managers.Resource.GetColorBlockSprite(blockdata.colorBlock_name, Define.ClickState.Clicked);
    }
}
