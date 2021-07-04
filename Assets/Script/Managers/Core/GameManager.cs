using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    public float time;
    
    public int total_score;
    public int combo;
    public int level;
    public Define.GameState gameState;
    public Define.GameState_Score gameState_score;

    public bool isGameOver;

    public Define.Column prevClickedCol;
    public SelectedFrame selectedFrame;

    public void Init()
    {
        time = Define.START_SECONDS;

        total_score = 0;
        combo = -1;
        level = (int)Define.Block.Blue;
        gameState = Define.GameState.Normal;
        gameState_score = Define.GameState_Score.Normal;

        isGameOver = false;

        prevClickedCol = Define.Column.None;

        if (selectedFrame != null) return;
        GameObject prefab = Managers.Resource.Get_Selected_Frame_Prefab();
        GameObject go = Object.Instantiate(prefab);
        selectedFrame = go.GetComponent<SelectedFrame>();
        selectedFrame.Init();
    }

    public Block Instantiate(Transform parent = null)
    {
        return Managers.Pool.Pop(parent);
    }

    public void Destroy(Block block)
    {
        if (block == null)
            return;
        Managers.Pool.Push(block);
    }
}
