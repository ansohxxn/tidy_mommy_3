using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    private float offset = 0f;
    public float speed;

    private MeshRenderer render;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        offset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
