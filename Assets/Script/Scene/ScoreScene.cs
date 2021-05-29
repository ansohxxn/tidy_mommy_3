﻿using System.Collections;
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
        if (new_record == null) new_record = Managers.Resource.GetGameResultSprite(Define.Result.Win);
        if (good_job == null) good_job = Managers.Resource.GetGameResultSprite(Define.Result.Lose);

        scoreTxt.text = "0";
        bestScoreTxt.text = string.Format("{0:#,##0}", Managers.Data.bestScore);
        floatingText.gameObject.SetActive(false);

        StartCoroutine(CountUpScore()); 
    }

    IEnumerator CountUpScore()
    {
        yield return Managers.Co.WaitSeconds(startDelay);

        float target = (float)Managers.Game.score;
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
        if (Managers.Game.score > Managers.Data.bestScore)
        {
            ShowFloatingText(new_record);
            Managers.Data.UpdateBestScore(Managers.Game.score);
        }
        else
            ShowFloatingText(good_job);

        PlayEndSFX();
    }

    private void ShowFloatingText(Sprite _sprite)
    {
        floatingText.gameObject.SetActive(true);
        floatingText.sprite = _sprite;
    }

    private void PlayEndSFX()
    {
        Managers.Audio.sfx_audioSource.PlayOneShot(Managers.Resource.GetSFX_End());
    }

    public override void Clear()
    {
        bestScoreTxt.text = "0";
        scoreTxt.text = "0";
        floatingText.sprite = null;
        floatingText.gameObject.SetActive(false);
    }
}