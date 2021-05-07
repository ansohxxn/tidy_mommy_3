using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum GameStateCombo
    {
        Fever = 5,
        SuperFever = 10
    }

    public enum GameStateScore
    {
        Normal = 1,
        Fever = 2,
        SuperFever = 4
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
