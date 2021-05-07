using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombGauge : MonoBehaviour
{
    public int maxBomb
    {
        get { return 10; }
        private set { value = 10; }
    }
    [HideInInspector] public Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = maxBomb;
        slider.value = 0;
    }
}
