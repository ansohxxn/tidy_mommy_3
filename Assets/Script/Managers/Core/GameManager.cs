﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager
{
    public float time;
    const float startSeconds = 60f;

    public int score;
    public int combo = -1;
    public int level = (int)Define.ColorBlock.Blue;
    public Define.GameState gameState = Define.GameState.Normal;
    public Define.GameState_Score gameState_score = Define.GameState_Score.Normal;

    public bool isGameOver;

    public Define.Column prevClickedCol = Define.Column.None;
    public SelectedFrame selectedFrame;

    public void Init()
    {
        time = startSeconds;
        isGameOver = false;

        if (selectedFrame != null) return;
        GameObject prefab = Managers.Resource.GetSelectedFramePrefab();
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
