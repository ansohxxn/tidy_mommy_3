using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EraseCommand : BasicCommand
{
    public override bool CanRemove(int col, int row)
    {
        return true;
    }

    public override void Clear(int col, int row)
    {
        Clear_All_Blocks();
    }
}
