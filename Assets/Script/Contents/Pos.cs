using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos : MonoBehaviour
{
    float width, height;
    Vector2 startPos;

    public Transform[,] blockPos = new Transform[Define.MAX_COL_NUM, Define.MAX_ROW_NUM];
    public ParticleSystem[,] particlePos = new ParticleSystem[Define.MAX_COL_NUM, Define.MAX_ROW_NUM];

    SpriteRenderer background;
    [HideInInspector] public Transform myTransform;
    [SerializeField] GameObject prefab;

    void Start()
    {
        background = GetComponent<SpriteRenderer>();
        myTransform = background.GetComponent<Transform>();

        width = background.bounds.size.x / background.transform.localScale.x;
        height = background.bounds.size.y / background.transform.localScale.y; ;

        float startX = (-1) * (width / Define.MAX_COL_NUM);
        float startY = ((Define.MAX_ROW_NUM - 1) * height / ((Define.MAX_COL_NUM - 1) * Define.MAX_ROW_NUM));
        startPos = new Vector2(startX, startY);

        Managers.Game.selectedFrame.myTransform.position = startPos;
        Managers.Game.selectedFrame.myTransform.SetParent(myTransform);

        for (int i = 0; i < Define.MAX_COL_NUM; ++i)
        {
            float x = startPos.x + i * (width / Define.MAX_COL_NUM);
            for (int j = 0; j < Define.MAX_ROW_NUM; ++j)
            {
                float y = startPos.y - j * (height / Define.MAX_ROW_NUM);

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
