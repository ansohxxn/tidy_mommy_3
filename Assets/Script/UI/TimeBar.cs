using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeBar : MonoBehaviour
{
    const float second = 1f;
    const float epsilon = 0.01f;

    Slider slider;
    public TextMeshProUGUI timerText;
    [SerializeField] GameOverText gameover_text;

    void Start()
    {
        Init();
        TimeUpdate();
    }

    private void Init()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = Managers.Game.time;
        slider.value = Managers.Game.time;
    }

    private void TimeUpdate()
    {
        StartCoroutine(TimeTextUpdate());
        StartCoroutine(TimeSliderUpdate());
    }

    private IEnumerator TimeTextUpdate()
    {
        while (Managers.Game.time > 0f && !Managers.Game.isGameOver)
        {
            Managers.Game.time -= 1f;
            int nextTime = (int)Managers.Game.time;
            timerText.text = nextTime.ToString();

            yield return Managers.Co.WaitSeconds(second);
        }
        Managers.Game.isGameOver = true;
        Time.timeScale = 0;
        gameover_text.Set_GameOver_Text();
    }

    private IEnumerator TimeSliderUpdate()
    {
        while (slider.value <= 60 && Mathf.Abs(slider.value - Managers.Game.time) > epsilon && !Managers.Game.isGameOver)
        {
            slider.value = Mathf.Lerp(slider.value, Managers.Game.time, Time.deltaTime);
            yield return null;
        }

        if (Managers.Game.time == 0f)
            slider.value = 0f;
    }
}
