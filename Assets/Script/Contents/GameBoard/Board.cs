using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [HideInInspector] public List<List<Block>> blocks = new List<List<Block>>(); // 블록 객체들 (풀로 관리되는)
    [HideInInspector] public float one_turn_score = 0;

    int row_count_for_level = 0;
    int moveCount = 0;
    bool canMakeBomb = false;

    delegate void OnClear(int col, int row);

    [HideInInspector] public Pos pos;


    #region components
    Bomb bomb = new Bomb();

    ColorBlockBuilder colorBlockBuilder = new ColorBlockBuilder();
    SpecialBlcokBuilder specialBlcokBuilder = new SpecialBlcokBuilder();

    Pick_Block pick_block = new Pick_Block();
    Pick_Color_Block pick_color_block = new Pick_Color_Block();
    Pick_Special_Block pick_special_block = new Pick_Special_Block();

    BasicCommand command;
    ColorCommand colorCommand;
    RainbowCommand rainbowCommand;
    EraseCommand eraseCommand;
    UpCommand upCommand;
    OneCommand oneCommand;
    BombCommand bombCommand;

    [SerializeField] ScoreText score_text;
    [SerializeField] tenSecondsImg tenSecondsImg;
    [SerializeField] BombGauge bomb_gauge;
    [SerializeField] GameOverText gameover_text;
    GameMode gameMode;
    #endregion

    void Start()
    {
        Get_Component_Init();
        Board_Init();

        GameLoop();
    }

    private void Get_Component_Init()
    {
        pos = GetComponent<Pos>();
        gameMode = GetComponent<GameMode>();

        colorCommand = GetComponent<ColorCommand>();
        rainbowCommand = GetComponent<RainbowCommand>();
        eraseCommand = GetComponent<EraseCommand>();
        upCommand = GetComponent<UpCommand>();
        oneCommand = GetComponent<OneCommand>();
        bombCommand = GetComponent<BombCommand>();
    }

    public void Board_Init()  // 필요한 초기화 작업
    {
        if (blocks.Count < Define.MAX_COL_NUM)
            for (int i = 0; i < Define.MAX_COL_NUM; ++i)
                blocks.Add(new List<Block>());

        for (int i = 0; i < Define.START_ROW_NUM; ++i)
            Make_Next_Rows();
    }
    
    private void GameLoop() // 게임 진행
    {
        StartCoroutine(Periodically_Spawn());
    }

    private IEnumerator Periodically_Spawn()  // 정해진 시간 주기마다 한 줄(한 행)씩 생성한다. 
    {
        while (true)
        {
            yield return Managers.Co.WaitSeconds(Define.ROW_GENERATE_TIME);
            if (Managers.Game.isGameOver) 
                yield break;
            Make_Next_Rows();
        }
    }

    public void Make_Next_Rows() // 한 줄 생성 (블록 이중 리스트 업데이트 + 업데이트 된 리스트에 따른 위치 재정렬 + num_of_rows_to_change_level 줄 단위마다 레벨 상승)
    {
        for(int i = 0; i < Define.MAX_COL_NUM; ++i)
        {
            if (blocks[i].Count == Define.MAX_ROW_NUM)
            {
                GameOver();
                return;
            }
        }    
        Add_Row();
        Update_All_Row_Pos();
        Level_Update();
    }

    private void Add_Row()  // 한 줄(한 행) 추가. 한 행에 들어갈 3 개의 블록을 랜덤으로 정한다. 생성할 블록을 3 행이 같은 색이 될 소지가 있는 블록은 제외하고 뽑는다.
    {
        int bombCol = bomb.Get_Index_To_Put_Bomb(blocks, pos.myTransform, ref canMakeBomb);

        for (int i = 0; i < Define.MAX_COL_NUM; ++i)
        {
            if (i == bombCol) continue;
            if (Get_Last_Row_Block_Index(i) >= Define.MAX_ROW_NUM) continue;

            Make_Block(i);
        }
    }

    private void Make_Block(int col)
    {
        Block block = Managers.Game.Instantiate(pos.myTransform);

        Define.BlockType blockType = pick_block.Get_Randomly_ColorBlock_Or_SpecialBlock();
        int RandomBlockID = 0;
        switch (blockType)
        {
            case Define.BlockType.Color:
                RandomBlockID = pick_color_block.Get_Random_Block(blocks, col);
                block.blockBuilder = colorBlockBuilder;
                break;

            case Define.BlockType.Special:
                RandomBlockID = pick_special_block.Get_Random_Block();
                block.blockBuilder = specialBlcokBuilder;
                break;
        }

        block.blockBuilder.Block_OnEnable(block, (Define.Block)RandomBlockID);
        blocks[col].Insert(0, block);
    }

    
    private void Update_All_Row_Pos() // 업데이트 된 block 리스트 인덱스에 따른 블록들의 위치 업데이트
    {
        if (Managers.Game.isGameOver) return;

        for (int i = 0; i < Define.MAX_COL_NUM; ++i)
        {
            for (int j = 0; j < blocks[i].Count; ++j)
                blocks[i][j].myTransform.position = pos.blockPos[i, j].position;
        }
        Update_Selected_Frame_Pos();
    }

    
    private void Update_Selected_Frame_Pos() // 시간 주기마다 행이 추가로 생성되어 내려오기 때문에 선택되어 있는 프레임도 같이 한 칸 내려와야 한다.
    {
        if (Managers.Game.isGameOver || Managers.Game.prevClickedCol == Define.Column.None)
            return;

        int col = (int)Managers.Game.prevClickedCol;
        int row = Get_Last_Row_Block_Index(col);
        Block lastBlock = GetLastRowBlock(col);
        Managers.Game.selectedFrame.Setting(lastBlock, pos.blockPos[col, row]);
    }

    private bool Can_Move(int nextCol)
    {
        if (Managers.Game.isGameOver)
            return false;

        if (Managers.Game.selectedFrame.selectedBlock == null)
            return false;

        if (blocks[nextCol].Count >= Define.MAX_ROW_NUM)
            return false;

        return true;
    }

    private void Block_Move(Block block, int prevCol, int nextCol)
    {
        moveCount++;

        blocks[prevCol].RemoveAt(Get_Last_Row_Block_Index(prevCol));
        blocks[nextCol].Add(block);
        block.myTransform.position = pos.blockPos[nextCol, Get_Last_Row_Block_Index(nextCol)].position;

        Managers.Audio.Play_SFX(Define.SFX.Move);
    }

    public void Move_And_Remove(Block block, int prevCol, int nextCol) // 블록 이동시키기 + 제거(제거 가능한지 검사도 같이함) + 위치 재정렬  
    {
        if (!Can_Move(nextCol)) 
            return;
       
        Managers.Game.prevClickedCol = Define.Column.None;

        // Move
        Block_Move(block, prevCol, nextCol);

        // Check & Remove
        int nextRow = Get_Last_Row_Block_Index(nextCol);
        Check_Block_Type(block, nextCol, nextRow);
        if (command.CanRemove(nextCol, nextRow))
            Remove_Block(command.Clear, nextCol, nextRow);

        // Pos Update
        Update_All_Row_Pos();
    }

    private void Check_Block_Type(Block block, int col, int row)
    {
        if (block.blockdata.blockType == Define.BlockType.Color)
            command = colorCommand;
        else
        {
            switch(block.blockdata.block_name)
            {
                case Define.Block.Rainbow:
                    command = rainbowCommand;
                    break;
                case Define.Block.Erase:
                    command = eraseCommand;
                    break;
                case Define.Block.Up:
                    command = upCommand;
                    break;
                case Define.Block.One:
                    command = oneCommand;
                    break;
                case Define.Block.Bomb:
                    command = bombCommand;
                    break;
            }
        }
    }

    private void Remove_Block(OnClear onClear, int col, int row)
    {
        onClear(col, row);

        Update_Combo(col, row);
        Calculate_Score();
        bomb_gauge.SetBombGauge(ref canMakeBomb);
    }

    public void Clear_Init_Board()
    {
        command.Clear_All_Blocks();

        moveCount = 0;
        Calculate_Score();
        bomb_gauge.SetBombGauge(ref canMakeBomb);
    }

    public void Show_Bomb_Seconds_Img(Vector2 pos)
    {
        tenSecondsImg.myGameObject.SetActive(true);
        tenSecondsImg.Show_TenSecondsImg(pos);
    }

    private void Calculate_Score()
    {
        one_turn_score *= (int)Managers.Game.gameState_score;
        one_turn_score *= (Managers.Game.combo / 100f + 1);
        Managers.Game.total_score += (int)one_turn_score;

        one_turn_score = 0f;

        score_text.Text_UI_Update();
    }

    private void Level_Update()
    {
        ++row_count_for_level;
        if (row_count_for_level == Define.NUM_OF_ROWS_TO_CHANGE_LEVEL)
        {
            if(Managers.Game.level < Define.NUM_OF_COLOR_BLOCK - 1)
                Managers.Game.level += 1;
            row_count_for_level = 0;
        }
    }

    private void Update_Combo(int col, int row)
    {
        gameMode.Update_Combo(moveCount, pos.blockPos[col, row].position);
        moveCount = 0;
    }

    public void GameOver()
    {
        Managers.Game.isGameOver = true;

        Clear_Init_Board();
        gameover_text.Set_GameOver_Text();
        Managers.Game.selectedFrame.SetDisactive();

        StartCoroutine(Wait_And_Exit());
    }

    IEnumerator Wait_And_Exit()
    {
        yield return Managers.Co.WaitSeconds(Define.WAIT_EXIT_TIME);
        Managers.Scene.LoadScene(Define.Scene.Loading);
    }

    public Block GetLastRowBlock(int col)
    {
        if (blocks[col].Count == 0) return null;
        return blocks[col][blocks[col].Count - 1];
    }
    public bool IsBlockEmpty(int col)
    {
        if (blocks[col].Count == 0) return true;
        return false;
    }
    public int Get_Last_Row_Block_Index(int col)
    {
        return blocks[col].Count - 1;
    }
}