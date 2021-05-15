using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    Pos pos;
    List<List<Block>> blocks = new List<List<Block>>(); // 블록 객체들 (풀로 관리되는)

    float rowGenerateTime = 4.0f;
    int rowCount = 0;
    const int num_of_rows_to_change_level = 20;
    float tmp_score = 0;
    bool canMakeBomb = false;
    int action = 0;

    const int maxColNum = 3;
    const int maxRowNum = 10;
    [SerializeField] int startRowNum = 4;

    int blockColorNum = 0;
    int blockSpecialNum = 0;
    const float blockColorProb = 0.98f;
    List<int> randomBox = new List<int>();

    int moveCount = 0;
    int combo = 0;

    [SerializeField] ScoreText score_text;
    [SerializeField] ComboText comboText;
    [SerializeField] BombGauge bomb_gauge;
    [SerializeField] GameOverText gameover_text;

    public Block GetLastRowBlock(int col) { return blocks[col][blocks[col].Count - 1]; }
    public int GetLastRowBlock_Index(int col) { return blocks[col].Count - 1; }

    void Start()
    {
        Init();
        // Ready Go ! (코루틴)
        StartCoroutine(PeriodicallySpawn());
    }

    // 필요한 초기화 작업
    private void Init()
    {
        pos = GetComponent<Pos>();

        blockColorNum = System.Enum.GetValues(typeof(Define.ColorBlock)).Length;
        blockSpecialNum = System.Enum.GetValues(typeof(Define.SpecialBlock)).Length;
        
        for (int i = 0; i < maxColNum; ++i)
            blocks.Add(new List<Block>());

        for (int i = 0; i < startRowNum; ++i)
            MakeNextRows();
    }

    // 정해진 시간 주기마다 한 줄(한 행)씩 생성한다. 
    public IEnumerator PeriodicallySpawn()
    {
        while (true)
        {
            yield return Managers.Co.WaitSeconds(rowGenerateTime);
            if (Managers.Game.isGameOver) 
                yield break;
            MakeNextRows();
        }
    }

    // 한 줄 생성 (블록 이중 리스트 업데이트 + 업데이트 된 리스트에 따른 위치 재정렬 + num_of_rows_to_change_level 줄 단위마다 레벨 상승)
    public void MakeNextRows()
    {
        AddRow();
        UpdateAllRowPos();
        LevelUpdate();
    }

    // 색깔 블록 or 스페셜 블록 뽑기 (확률이 다르다) 
    private Define.BlockType BasicBlock_Or_SpecialBlock()
    {
        float res = Random.Range(0f, 1f);
        if (res < blockColorProb)
            return Define.BlockType.Color;
        else return Define.BlockType.Special;
    }

    // 색깔 블록 랜덤 뽑기. except 블록은 제외하고 뽑는다. 현재 레벨 내의 블록들만 뽑힐 수 있다.  
    private int RandomColorBlock(int except)
    {
        randomBox.Clear();
        int max = Managers.Game.level;
        for (int i = 0; i <= max; ++i)
            if (i != except)
                randomBox.Add(i);
        int res = randomBox[Random.Range(0, randomBox.Count)];
        return res;
    }

    private int MakeBomb()
    {
        if (canMakeBomb)
        {
            canMakeBomb = false;

            Block block = Managers.Game.Instantiate(pos.background_transform);
            block.SpecialBlock_OnEnable(Define.SpecialBlock.Bomb);
            
            int bombCol = Random.Range(0, maxColNum);

            blocks[bombCol].Insert(0, block);
            return bombCol;
        }
        return -1;
    }

    // 한 줄(한 행) 추가. 한 행에 들어갈 3 개의 블록을 랜덤으로 정한다. 생성할 블록을 3 행이 같은 색이 될 소지가 있는 블록은 제외하고 뽑는다.
    private void AddRow()
    {
        int bombCol = MakeBomb();

        for (int i = 0; i < maxColNum; ++i)
        {
            if (i == bombCol) continue;
            if (GetLastRowBlock_Index(i) >= 10) continue;

            int RandomBlockID = 0;
            Block block = Managers.Game.Instantiate(pos.background_transform);

            int except = -1;
            Define.BlockType blockType = BasicBlock_Or_SpecialBlock();
            switch (blockType)
            {
                case Define.BlockType.Color:
                    if (blocks[i].Count >= 2)
                    {
                        if (blocks[i][0].blockdata.blockType == Define.BlockType.Color &&
                            blocks[i][1].blockdata.blockType == Define.BlockType.Color &&
                            blocks[i][0].blockdata.colorBlock_name == blocks[i][1].blockdata.colorBlock_name)
                            except = (int)blocks[i][0].blockdata.colorBlock_name;
                    }
                    RandomBlockID = RandomColorBlock(except);
                    block.ColorBlock_OnEnable((Define.ColorBlock)RandomBlockID);
                    break;

                case Define.BlockType.Special:
                    //RandomBlockID = 4;
                    RandomBlockID = Random.Range(0, blockSpecialNum - 1); // BOMB 은 제외하고 뽑기 (BOMB 블록은 폭탄 게이지를 채웠을 때만 생성되기 때문)
                    block.SpecialBlock_OnEnable((Define.SpecialBlock)RandomBlockID);
                    break;
            }  
            blocks[i].Insert(0, block);
        }
    }

    // 업데이트 된 block 리스트 인덱스에 따른 블록들의 위치 업데이트
    private void UpdateAllRowPos()
    {
        if (Managers.Game.isGameOver) return;

        for (int i = 0; i < maxColNum; ++i)
        {
            if (blocks[i].Count > maxRowNum)
            {
                Managers.Game.isGameOver = true;
                gameover_text.Set_GameOver_Text();
                return;
            }

            for (int j = 0; j < blocks[i].Count; ++j)
                blocks[i][j].myTransform.position = pos.blockPos[i, j].position;
        }
        UpdateSelectedFramePos();
    }

    // 시간 주기마다 행이 추가로 생성되어 내려오기 때문에 선택되어 있는 프레임도 같이 한 칸 내려와야 한다.
    private void UpdateSelectedFramePos()
    {
        if (Managers.Game.isGameOver) return;
        if (Managers.Game.prevClickedCol == Define.Column.None) return;

        Managers.Game.selectedFrame.selectedBlock.SetBasicStateSprite();
        int col = (int)Managers.Game.prevClickedCol;
        int row = GetLastRowBlock_Index(col);
        Block lastBlock = GetLastRowBlock(col);
        Managers.Game.selectedFrame.Setting(lastBlock, pos.blockPos[col, row]);
    }

    private void Clear()
    {
        Calculate_Score();
        SetBombGauge();
    }

    // 블록 이동시키기 + 제거(제거 가능한지 검사도 같이함) + 위치 재정렬  
    public void MoveAndCheck(Block block, int prevCol, int nextCol)
    {
        if (Managers.Game.isGameOver) return;
        Managers.Game.prevClickedCol = Define.Column.None;

        // Move
        blocks[prevCol].RemoveAt(GetLastRowBlock_Index(prevCol));
        blocks[nextCol].Add(block);
        int startRow = GetLastRowBlock_Index(nextCol);
        if (startRow >= maxRowNum) return;
        block.myTransform.position = pos.blockPos[nextCol, startRow].position;
        moveCount++; // 이동 횟수 증가!

        // Check
        if (block.blockdata.blockType == Define.BlockType.Color)
            Clear_Color_Block(nextCol, startRow);
        else
            Clear_Special_Block(nextCol, startRow, block.blockdata.specialBlock_name);

        // Pos Update
        UpdateAllRowPos();
    }

    // 하나의 블록을 제거할 때 사용할 함수
    private void Clear_One_Block(int col, int row)
    {
        // 블록 당 점수
        tmp_score += blocks[col][row].blockdata.score;

        // 만약 폭탄 블록인 경우 10 초 추가, 폭탄 게이지 리셋
        if (blocks[col][row].blockdata.specialBlock_name == Define.SpecialBlock.Bomb)
            Managers.Game.time += 10f;

        // 제거
        Managers.Game.Destroy(blocks[col][row]);
        blocks[col].RemoveAt(row);
    }

    // Bomb, Rainbow 같이 중간 행 블록을 제거하는 블록의 경우엔 재정렬시 같은 색 블록 3 행이 될 수도 있다. 이런 경우도 다 체크하고 제거하는 함수.
    private void Clear_Additional_Blocks()
    {
        for (int i = 0; i < maxColNum; ++i)
        {
            bool first = true;
            int count = 1;
            Define.ColorBlock cmpColor = Define.ColorBlock.Red;
            for (int j = 0; j < blocks[i].Count; )
            {
                if (first || cmpColor != blocks[i][j].blockdata.colorBlock_name || blocks[i][j].blockdata.blockType == Define.BlockType.Special)
                {
                    first = false;
                    cmpColor = blocks[i][j].blockdata.colorBlock_name;
                    count = 1;
                    ++j;
                }
                else if (++count == 3)
                {
                    Clear_One_Block(i, j);
                    Clear_One_Block(i, j - 1);
                    Clear_One_Block(i, j - 2);
                    count = 1;
                }
                else
                    ++j;
            }
        }
    }

    // 이동시킨 컬러 블록이 이동함으로써 3 행이 동일한 색깔이 되었다면 제거할 수 있다.
    private void Clear_Color_Block(int col, int row)
    {
        Block block = blocks[col][row];
        if (blocks[col].Count < 3) return;
        if (blocks[col][row - 1].blockdata.blockType != Define.BlockType.Color || blocks[col][row - 2].blockdata.blockType != Define.BlockType.Color) return;
        if (blocks[col][row - 1].blockdata.colorBlock_name != block.blockdata.colorBlock_name ||
            blocks[col][row - 2].blockdata.colorBlock_name != block.blockdata.colorBlock_name) return;

        for (int i = 0; i < 3; ++i)
            Clear_One_Block(col, row - i);

        UpdateCombo(col, row);
        Clear();
    }

    // 이동시킨 스페셜 블록 종류에 따른 제거 로직
    private void Clear_Special_Block(int col, int row, Define.SpecialBlock specialBlock)
    {
        switch (specialBlock)
        {
            case Define.SpecialBlock.Rainbow:
                Clear_Rainow_Special_Block(col, row);
                break;
            case Define.SpecialBlock.Erase:
                Clear_Erase_Special_Block();
                break;
            case Define.SpecialBlock.Up:
                Clear_Up_Special_Block(col, row);
                break;
            case Define.SpecialBlock.One:
                Clear_One_Special_Block(col, row);
                break;
            case Define.SpecialBlock.Bomb:
                Clear_Bomb_Special_Block(col, row);
                break;
        }
    }

    // Rainbow 블록 : 이동하여 맞 닿게 된 블록과 동일한 색깔을 가진 블록을 전부 없앤다. 
    private void Clear_Rainow_Special_Block(int col, int row)
    {
        Block attached_block = blocks[col][row - 1];
        if (attached_block.blockdata.blockType == Define.BlockType.Special)
            return;

        Clear_One_Block(col, row);

        for (int i = 0; i < maxColNum; ++i)
        {
            for (int j = 0; j < blocks[i].Count; )
            {
                if (blocks[i][j].blockdata.blockType == Define.BlockType.Color && attached_block.blockdata.colorBlock_name == blocks[i][j].blockdata.colorBlock_name) 
                    Clear_One_Block(i, j);
                else 
                    ++j;
            }
        }

        Clear_Additional_Blocks();

        UpdateCombo(col, row);
        Clear();
    }

    // Erase 블록 : 모든 블록을 없앤다. 없어지는 범위에 폭탄 블록이 있을 경우 발동시킨다.
    private void Clear_Erase_Special_Block()
    {
        for (int i = 0; i < maxColNum; ++i)
        {
            for (int j = 0; j < blocks[i].Count; )
            {
                Clear_One_Block(i, j);
            }
        }

        UpdateCombo(5, 5);
        Clear();
    }

    // Up 블록 : 이동한 곳의 한 열을 전부 없앤다. 없어지는 범위에 스페셜 블록이 있을 경우 발동시키지 않고 보존한다.
    private void Clear_Up_Special_Block(int col, int row)
    {
        Clear_One_Block(col, row);
        for (int i = 0; i < blocks[col].Count; )
        {
            if (blocks[col][i].blockdata.blockType == Define.BlockType.Color)
                Clear_One_Block(col, i);
            else
                ++i;
        }

        UpdateCombo(col, row);
        Clear();
    }

    // one 블록 : 이동한 곳에 있는 하나의 블록만 없앤다. 해당 블록이 스페셜 블록일 경우 발동시키지 않고 보존한다.
    private void Clear_One_Special_Block(int col, int row)
    {
        Block attached_block = blocks[col][row - 1];
        if (attached_block.blockdata.blockType == Define.BlockType.Special)
            return;
        Clear_One_Block(col, row);
        Clear_One_Block(col, row - 1);

        UpdateCombo(col, row);
        Clear();
    }

    // bomb 블록 : 폭탄이 터진 열 기준에서의 양 옆의 열의 3 행을 없앤다. 최대 3 x 3 크기를 없앤다. 게임 시간 10 초를 추가한다. 폭탄이 터지는 범위에 스페셜 블록이 있을 경우 발동시키지 않고 보존한다.
    private void Clear_Bomb_Special_Block(int col, int row)
    {
        Clear_One_Block(col, row);

        for (int i = row - 1, count = 0; i >= 0 && count < 2 && blocks[col].Count > i; ++count, --i)
        {
            if (blocks[col][i].blockdata.blockType == Define.BlockType.Color)
                Clear_One_Block(col, i);
        }

        if (col == 1 || col == 2)
        {
            if (blocks[col - 1].Count > row)
                Clear_One_Block(col - 1, row);

            for (int i = row - 1, count = 0; i >= 0 && count < 2 && blocks[col - 1].Count > i; ++count, --i)
            {
                if (blocks[col - 1][i].blockdata.blockType == Define.BlockType.Color)
                    Clear_One_Block(col - 1, i);
            }
        }

        if (col == 0 || col == 1)
        {
            if (blocks[col + 1].Count > row)
                Clear_One_Block(col + 1, row);

            for (int i = row - 1, count = 0; i >= 0 && count < 2 && blocks[col + 1].Count > i; ++count, --i)
            {
                if (blocks[col + 1][i].blockdata.blockType == Define.BlockType.Color)
                    Clear_One_Block(col + 1, i);
            }
        }

        Clear_Additional_Blocks();

        UpdateCombo(col, row);
        Clear();
    }

    private void Calculate_Score()
    {
        tmp_score *= (int)Managers.Game.gameState;
        tmp_score *= (combo / 100f + 1);
        Managers.Game.score += (int)tmp_score;

        tmp_score = 0f;

        score_text.Text_UI_Update();
    }

    private void SetBombGauge()
    {
        bomb_gauge.slider.value += 1;
        if (bomb_gauge.slider.value == bomb_gauge.maxBomb)
        {
            canMakeBomb = true;
            bomb_gauge.slider.value = 0;
        }
    }  

    private void LevelUpdate()
    {
        ++rowCount;
        if (rowCount == num_of_rows_to_change_level)
        {
            Managers.Game.level += 1;
            rowCount = 0;
        }
    }

    private void UpdateCombo(int col, int row)
    {
        if (moveCount <= Managers.Game.level + 1)
        {
            ++combo;
            if (combo > 1)
            {
                comboText.gameObject.SetActive(true);
                comboText.Show_ComboText(combo - 1, pos.blockPos[col, row].position);
            }
        }
        else
        {
            combo = 0;
        }
        moveCount = 0;
    }
}