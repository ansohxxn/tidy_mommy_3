using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum SFX
    {
        Move,
        Success,
        Click
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

    public enum ColorBlock
    {
        Red,
        Blue,
        Yellow,
        Green,
        Orange,
        Purple,
        White,
    }

    public enum SpecialBlock
    {
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

    public enum Sound
    {
        Bgm,
        Effect,
    }
}
