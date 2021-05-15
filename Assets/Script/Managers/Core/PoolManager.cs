using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager 
{
    const int poolSize = 30;

    public GameObject Prefab { get; private set; }
    public Transform Root { get; set; }

    Queue<Block> _poolQueue = new Queue<Block>();

    public void Init()
    {
        Prefab = Managers.Resource.GetBlockPrefab();
        Root = new GameObject().transform;
        Root.name = "@Pool_Root";

        for (int i = 0; i < poolSize; i++)
            Push(Create());
    }

    Block Create()
    {
        GameObject go = Object.Instantiate<GameObject>(Prefab);
        Block block = go.GetComponent<Block>();
        block.Init();
        return block;
    }

    public void Push(Block block)
    {
        if (block == null)
            return;

        block.myTransform.SetParent(Root);
        block.myGameObject.SetActive(false);

        _poolQueue.Enqueue(block);
    }

    public Block Pop(Transform parent)
    {
        Block block;

        if (_poolQueue.Count > 0)
            block = _poolQueue.Dequeue();
        else
            block = Create();

        block.Init();
        block.myGameObject.SetActive(true);
        block.myTransform.SetParent(parent);

        return block;
    }

    public void Clear()
    {
        foreach (Transform child in Root)
            GameObject.Destroy(child.gameObject);

        _poolQueue.Clear();
    }
}
