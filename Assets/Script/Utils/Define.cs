using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    #region constant
    public const int CAN_REMOVE_NUM = 3;
    public const int MAX_COL_NUM = 3;
    public const int MAX_ROW_NUM = 10;
    public const int START_ROW_NUM = 4;
    public const int NUM_OF_COLOR_BLOCK = 7;
    public const int NUM_OF_SPECIAL_BLOCK = 5;
    public const int MAX_BOMB = 10;
    public const float SPECIAL_BLOCK_PROBABILITY = 0.02f;

    public const int MAX_LEVEL = NUM_OF_COLOR_BLOCK;
    public const int NUM_OF_ROWS_TO_CHANGE_LEVEL = 20;

    public const float SUPER_FEVER_TIME = 10.0f;
    public const float WAIT_EXIT_TIME = 3.0f;
    public const float ROW_GENERATE_TIME = 4.0f;
    public const float START_SECONDS = 60f;

    public const int TIMEBAR_TOUCH_SCORE = 5000;
    
    public const int COMBO_TO_FEVER_MODE = 5;
    public const int COMBO_TO_SUPER_FEVER_MODE = 10;

    public const int POOL_SIZE = 30;

    public static readonly float[] BGM_SPEED = new float[3] { 1.0f, 1.1f, 1.2f };
    public const float DEFAULT_VOLUME = 0.5f;
    #endregion

    #region enum
    public enum Result
    {
        Win,
        Lose
    }

    public enum Scene
    {
        Start,
        Main,
        Loading,
        Score
    }

    public enum SFX
    {
        Move,
        Success,
        Click,
        End
    }

    public enum GameState_Score
    {
        Normal = 1,
        Fever = 2,
        SuperFever = 4
    }

    public enum GameState
    {
        Normal,
        Fever,
        SuperFever
    }

    public enum BlockType
    {
        Color,
        Special
    }

    public enum ClickState
    {
        NotClicked,
        Clicked,
    }

    public enum Block
    {
        Red,
        Blue,
        Yellow,
        Green,
        Orange,
        Purple,
        White,
        Rainbow,
        Erase,
        Up,
        One,
        Bomb,
    }

    public enum Column
    {
        First,
        Second,
        Third,
        None,
    }
    #endregion
}
