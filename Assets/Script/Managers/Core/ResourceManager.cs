using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.U2D;

public class ResourceManager 
{
    #region sprite
    SpriteAtlas atlas;
    SpriteAtlas atlas2;

    string[][] Block_Sprite_Name = new string[12][]
    {
        new string[2] {"red1_0", "red2_0" },
        new string[2] {"blue1_0", "blue2_0" },
        new string[2] {"yellow_0", "yellow2_0" },
        new string[2] {"green_0", "green2_0" },
        new string[2] {"orange_0", "orange2_0" },
        new string[2] {"purple_0", "purple2_0" },
        new string[2] {"white_0", "white2_0" },
        new string[1] { "rainbow_0" },
        new string[1] { "erase_0" },
        new string[1] { "up_0" },
        new string[1] { "one_0" },
        new string[1] { "bomb_0" },
    };
    string[] Char_Sprite_Name = new string[3] { "sun_idle_0", "moon_idle_0", "fever_idle_0" };
    string[] Background_Sprite_Name = new string[3] { "Normal", "Fever", "SuperFever" };
    string[] Result_Name = new string[2] { "new_record", "good_job" };
    string selected_Name = "select_0";

    private void Load_Sprite()
    {
        atlas = Resources.Load<SpriteAtlas>("Art/Sprite/SpriteAtlas");
        atlas2 = Resources.Load<SpriteAtlas>("Art/Sprite/StartAtlas");
    }

    public Sprite Get_Color_Block_Sprite(Define.Block block, Define.ClickState state = Define.ClickState.NotClicked)
    {
        return atlas.GetSprite(Block_Sprite_Name[(int)block][(int)state]); 
    }

    public Sprite Get_Special_Block_Sprite(Define.Block block)
    {
        return atlas.GetSprite(Block_Sprite_Name[(int)block][0]);
    }

    public Sprite Get_Selected_Block_Sprite()
    {
        return atlas.GetSprite(selected_Name);
    }

    public Sprite Get_Char_Sprite(Define.GameState gameState)
    {
        return atlas.GetSprite(Char_Sprite_Name[(int)gameState]);
    }

    public Sprite Get_Background_Sprite(Define.GameState gameState)
    {
        return atlas.GetSprite(Background_Sprite_Name[(int)gameState]);
    }

    public Sprite Get_Game_Result_Sprite(Define.Result gameResult)
    {
        return atlas2.GetSprite(Result_Name[(int)gameResult]);
    }
    #endregion

    # region scriptableObject
    Dictionary<Define.Block, Block_Data> block_datas = new Dictionary<Define.Block, Block_Data>();

    public Block_Data Get_Block_Data(Define.Block block) { return block_datas[block];}

    private void Load_ScriptableObject()
    {
        foreach (Define.Block block in Enum.GetValues(typeof(Define.Block)))
            block_datas[block] = Resources.Load<Block_Data>("ScriptableObject/Block/" + block.ToString());
    }
    #endregion

    #region prefab
    GameObject block_prefab;
    public GameObject Get_Block_Prefab() { return block_prefab; }

    GameObject selectedFrame_prefab;
    public GameObject Get_Selected_Frame_Prefab() { return selectedFrame_prefab; }
    
    private void Load_Prefab()
    {
        block_prefab = Resources.Load<GameObject>("Prefab/Block");
        selectedFrame_prefab = Resources.Load<GameObject>("Prefab/SelectedFrame");
    }
    #endregion

    #region audio clip
    public Dictionary<Define.SFX, AudioClip> sfxAudioClips = new Dictionary<Define.SFX, AudioClip>();
    public AudioClip bgm;

    private void Load_Audio()
    {
        bgm = Resources.Load<AudioClip>("Sound/BGM/Root - Silent Partner");

        foreach (Define.SFX sfx in Enum.GetValues(typeof(Define.SFX)))
            sfxAudioClips[sfx] = Resources.Load<AudioClip>("Sound/SFX/" + sfx.ToString());
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
