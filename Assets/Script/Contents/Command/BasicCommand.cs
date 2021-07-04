using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BasicCommand : MonoBehaviour
{
    protected Board board;

    public abstract void Clear(int col, int row);
    public abstract bool CanRemove(int col, int row);

    void Start()
    {
        board = GetComponent<Board>();
    }

    public void Clear_One_Block(int col, int row) // 하나의 블록을 제거할 때 사용할 함수
    {
        // 블록 당 점수
        board.one_turn_score += board.blocks[col][row].blockdata.score;

        if (Managers.Game.isGameOver)
            return;

        // 만약 폭탄 블록인 경우 10 초 추가, 폭탄 게이지 리셋
        if (board.blocks[col][row].blockdata.block_name == Define.Block.Bomb)
        {
            board.Show_Bomb_Seconds_Img(board.pos.blockPos[col, row].position);
            Managers.Game.time += 10f;
        }

        // 제거
        board.pos.particlePos[col, row].Play();
        Managers.Game.Destroy(board.blocks[col][row]);
        board.blocks[col].RemoveAt(row);
        Managers.Audio.Play_SFX(Define.SFX.Success);
    }

    public void Clear_Additional_Blocks() // Bomb, Rainbow 같이 중간 행 블록을 제거하는 블록의 경우엔 재정렬시 같은 색 블록 3 행이 될 수도 있다. 이런 경우도 다 체크하고 제거하는 함수.
    {
        for (int i = 0; i < Define.MAX_COL_NUM; ++i)
        {
            int size = board.blocks[i].Count;
            bool first = true;
            int count = 1;
            Define.Block cmpColor = Define.Block.Red;
            for (int j = size - 1; j >= 0; --j)
            {
                if (first || cmpColor != board.blocks[i][j].blockdata.block_name || board.blocks[i][j].blockdata.blockType == Define.BlockType.Special)
                {
                    first = false;
                    cmpColor = board.blocks[i][j].blockdata.block_name;
                    count = 1;
                }
                else if (++count == 3)
                {
                    for (int k = 0; k < 3; ++k)
                        Clear_One_Block(i, j);
                    count = 1;
                }
            }
        }
    }

    public void Clear_All_Blocks()
    {
        for (int i = 0; i < Define.MAX_COL_NUM; ++i)
        {
            int size = board.blocks[i].Count;
            for (int j = size - 1; j >= 0; --j)
            {
                Clear_One_Block(i, j);
            }
        }
    }
}