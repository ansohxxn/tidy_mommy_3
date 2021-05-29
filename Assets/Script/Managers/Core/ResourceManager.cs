using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ResourceManager 
{
    #region sprite
    SpriteAtlas atlas;
    SpriteAtlas atlas2;

    string[,] ColorBlockSpriteName = new string[7, 2]
    {
        {"red1_0", "red2_0" },
        {"blue1_0", "blue2_0" },
        {"yellow_0", "yellow2_0" },
        {"green_0", "green2_0" },
        {"orange_0", "orange2_0" },
        {"purple_0", "purple2_0" },
        {"white_0", "white2_0" },
    };

    string[] SpecialBlockSpriteName = new string[5] { "rainbow_0", "erase_0", "up_0", "one_0", "bomb_0" };
    string[] CharSpriteName = new string[3] { "sun_idle_0", "moon_idle_0", "fever_idle_0" };
    string[] BackgroundSpritename = new string[3] { "Normal", "Fever", "SuperFever" };

    string[] ResultName = new string[2] { "new_record", "good_job" };

    string selectedName = "select_0";

    
    private void Load_Sprite()
    {
        atlas = Resources.Load<SpriteAtlas>("Art/Sprite/SpriteAtlas");
        atlas2 = Resources.Load<SpriteAtlas>("Art/Sprite/StartAtlas");
    }

    public Sprite GetColorBlockSprite(Define.ColorBlock block, Define.ClickState state = Define.ClickState.NotClicked)
    {
        Sprite s = atlas.GetSprite(ColorBlockSpriteName[3, 0]);
        return atlas.GetSprite(ColorBlockSpriteName[(int)block, (int)state]); 
    }

    public Sprite GetSpecialBlockSprite(Define.SpecialBlock block)
    {
        return atlas.GetSprite(SpecialBlockSpriteName[(int)block]);
    }

    public Sprite GetSelectedBlockSprite()
    {
        return atlas.GetSprite(selectedName);
    }

    public Sprite GetCharSprite(Define.GameState gameState)
    {
        return atlas.GetSprite(CharSpriteName[(int)gameState]);
    }

    public Sprite GetBackgroundSprite(Define.GameState gameState)
    {
        return atlas.GetSprite(BackgroundSpritename[(int)gameState]);
    }

    public Sprite GetGameResultSprite(Define.Result gameResult)
    {
        return atlas2.GetSprite(ResultName[(int)gameResult]);
    }
    #endregion

    # region scriptableObject
    Dictionary<Define.ColorBlock, Block_Data> color_block_data = new Dictionary<Define.ColorBlock, Block_Data>();
    Dictionary<Define.SpecialBlock, Block_Data> special_block_data = new Dictionary<Define.SpecialBlock, Block_Data>();

    public Block_Data GetColorBlockData(Define.ColorBlock block) { return color_block_data[block];}
    public Block_Data GetSpecialBlockData(Define.SpecialBlock block) { return special_block_data[block]; }

    private void Load_ScriptableObject()
    {
        color_block_data[Define.ColorBlock.Red] = Resources.Load<Block_Data>("ScriptableObject/Block/Red");
        color_block_data[Define.ColorBlock.Blue] = Resources.Load<Block_Data>("ScriptableObject/Block/Blue");
        color_block_data[Define.ColorBlock.Yellow] = Resources.Load<Block_Data>("ScriptableObject/Block/Yellow");
        color_block_data[Define.ColorBlock.Green] = Resources.Load<Block_Data>("ScriptableObject/Block/Green");
        color_block_data[Define.ColorBlock.Orange] = Resources.Load<Block_Data>("ScriptableObject/Block/Orange");
        color_block_data[Define.ColorBlock.Purple] = Resources.Load<Block_Data>("ScriptableObject/Block/Purple");
        color_block_data[Define.ColorBlock.White] = Resources.Load<Block_Data>("ScriptableObject/Block/White");

        special_block_data[Define.SpecialBlock.Rainbow] = Resources.Load<Block_Data>("ScriptableObject/SpecialBlock/Rainbow");
        special_block_data[Define.SpecialBlock.Erase] = Resources.Load<Block_Data>("ScriptableObject/SpecialBlock/Erase");
        special_block_data[Define.SpecialBlock.One] = Resources.Load<Block_Data>("ScriptableObject/SpecialBlock/One");
        special_block_data[Define.SpecialBlock.Up] = Resources.Load<Block_Data>("ScriptableObject/SpecialBlock/Up");
        special_block_data[Define.SpecialBlock.Bomb] = Resources.Load<Block_Data>("ScriptableObject/SpecialBlock/Bomb");
    }
    #endregion

    #region prefab
    GameObject block_prefab;
    public GameObject GetBlockPrefab() { return block_prefab; }

    GameObject selectedFrame_prefab;
    public GameObject GetSelectedFramePrefab() { return selectedFrame_prefab; }
    
    private void Load_Prefab()
    {
        block_prefab = Resources.Load<GameObject>("Prefab/Block");
        selectedFrame_prefab = Resources.Load<GameObject>("Prefab/SelectedFrame");
    }
    #endregion

    #region audio clip
    AudioClip bgm;
    AudioClip sfx_move;
    AudioClip sfx_success;
    AudioClip sfx_click;
    AudioClip sfx_end;
    public AudioClip GetBGM() { return bgm; }
    public AudioClip GetSFX_Move() { return sfx_move; }
    public AudioClip GetSFX_Success() { return sfx_success; }
    public AudioClip GetSFX_Click() { return sfx_click; }
    public AudioClip GetSFX_End() { return sfx_end; }

    private void Load_Audio()
    {
        bgm = Resources.Load<AudioClip>("Sound/BGM/Root - Silent Partner");
        sfx_move = Resources.Load<AudioClip>("Sound/SFX/Move");
        sfx_success = Resources.Load<AudioClip>("Sound/SFX/Success");
        sfx_click = Resources.Load<AudioClip>("Sound/SFX/Click");
        sfx_end = Resources.Load<AudioClip>("Sound/SFX/End");
    }
    #endregion

    public void Init()
    {
        Load_Sprite();
        Load_ScriptableObject();
        Load_Prefab();
        Load_Audio();
    }
}
