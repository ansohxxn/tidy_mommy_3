using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Block", menuName = "New Block/block")]
public class Block_Data : ScriptableObject
{
    public Define.BlockType blockType;
    public Define.ColorBlock colorBlock_name;
    public Define.SpecialBlock specialBlock_name;
    public int score;
}
