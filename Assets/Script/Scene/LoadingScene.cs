using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : BaseScene
{
    [SerializeField] Slider slider;
    private AsyncOperation operation;

    protected override void Init()
    {
        StartCoroutine(LoadCoroutine());
    }

    public override void Clear()
    {

    }

    IEnumerator LoadCoroutine()
    {
        operation = Managers.Scene.LoadSceneAsync(Define.Scene.Score);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(operation.progress, 1f, timer);
                if (slider.value >= operation.progress)
                    timer = 0f;
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, 1f, timer);
                if (slider.value >= 0.99f)
                    operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
