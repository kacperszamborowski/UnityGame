using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMana(int maxMana)
    {
        slider.maxValue = maxMana;
    }

    public void SetMana(int currentMana)
    {
        slider.value = currentMana;

    }
}
