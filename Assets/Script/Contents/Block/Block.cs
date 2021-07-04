using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector] public Block_Data blockdata;
    [HideInInspector] public Transform myTransform;
    [HideInInspector] public GameObject myGameObject;
    [HideInInspector] public SpriteRenderer mySpriteRenderer;
    [HideInInspector] public IBlockBuilder blockBuilder;

    public void Init()
    {
        myTransform = GetComponent<Transform>();
        myGameObject = gameObject;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
