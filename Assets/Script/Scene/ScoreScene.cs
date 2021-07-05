using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScene : BaseScene
{
    [SerializeField] TextMeshPro bestScoreTxt;
    [SerializeField] TextMeshPro scoreTxt;
    [SerializeField] SpriteRenderer floatingText;
    private Sprite new_record;
    private Sprite good_job;

    private float duration = 3f;
    private float startDelay = 0.5f;

    protected override void Init()
    {
        if (new_record == null) new_record = Managers.Resource.Get_Game_Result_Sprite(Define.Result.Win);
        if (good_job == null) good_job = Managers.Resource.Get_Game_Result_Sprite(Define.Result.Lose);

        scoreTxt.text = "0";
        bestScoreTxt.text = string.Format("{0:#,##0}", Managers.Data.bestScore);
        floatingText.gameObject.SetActive(false);

        StartCoroutine(Count_Up_Score()); 
    }

    IEnumerator Count_Up_Score()
    {
        yield return Managers.Co.WaitSeconds(startDelay);

        float target = (float)Managers.Game.total_score;
        float current = 0;
        float offset = (target - current) / duration;
        while (current < target)
        {
            current += offset * Time.deltaTime;
            scoreTxt.text = ((int)current).ToString();
            yield return null;
        }

        current = target;
        scoreTxt.text = string.Format("{0:#,##0}", (int)current);
        SaveScore();
    }

    private void SaveScore()
    {
        if (Managers.Game.total_score > Managers.Data.bestScore)
        {
            Show_Floating_Text(new_record);
            Managers.Data.UpdateBestScore(Managers.Game.total_score);
        }
        else
            Show_Floating_Text(good_job);

        Managers.Audio.Play_SFX(Define.SFX.End);
    }

    private void Show_Floating_Text(Sprite _sprite)
    {
        floatingText.gameObject.SetActive(true);
        floatingText.sprite = _sprite;
    }

    public override void Clear()
    {
        bestScoreTxt.text = "0";
        scoreTxt.text = "0";
        floatingText.sprite = null;
        floatingText.gameObject.SetActive(false);
    }
}
