using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    float width, height;
    Vector2 startPos;

    public Transform[,] blockPos = new Transform[3, 10];
    public ParticleSystem[,] particlePos = new ParticleSystem[3, 10];

    SpriteRenderer background;
    [HideInInspector] public Transform myTransform;
    [SerializeField] GameObject prefab;

    void Start()
    {
        background = GetComponent<SpriteRenderer>();
        myTransform = background.GetComponent<Transform>();

        width = background.bounds.size.x / background.transform.localScale.x;
        height = background.bounds.size.y / background.transform.localScale.y; ;

        float startX = (-1) * (width / 3);
        float startY = (9 * height / (2 * 10));
        startPos = new Vector2(startX, startY);

        Managers.Game.selectedFrame.myTransform.position = startPos;
        Managers.Game.selectedFrame.myTransform.SetParent(myTransform);

        for (int i = 0; i < 3; ++i)
        {
            float x = startPos.x + i * (width / 3);
            for (int j = 0; j < 10; ++j)
            {
                float y = startPos.y - j * (height / 10);

                GameObject go = Object.Instantiate(prefab);
                blockPos[i, j] = go.transform;
                blockPos[i, j].SetParent(myTransform);

                Vector2 pos = new Vector2(x, y);
                blockPos[i, j].localPosition = pos;

                particlePos[i,j] = go.GetComponent<ParticleSystem>();
            }
        }
    }
}
