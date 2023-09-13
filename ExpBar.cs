using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public Slider slider;

    public void SetExpForLevel(int expForLevel)
    {
        slider.maxValue = expForLevel;
    }

    public void SetExp(int exp)
    {
        slider.value = exp;

    }
}
