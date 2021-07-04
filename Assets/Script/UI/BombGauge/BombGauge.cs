using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombGauge : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = Define.MAX_BOMB;
        slider.value = 0;
    }

    public void SetBombGauge(ref bool canMakeBomb)
    {
        slider.value += 1;
        if (slider.value == Define.MAX_BOMB)
        {
            canMakeBomb = true;
            slider.value = 0;
        }
    }
}
