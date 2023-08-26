using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX_Slider : MonoBehaviour
{
    private Slider S_Slider;



    public void Set_SFX_SliderValue(float value)
    {
        S_Slider.value = value;
        
    }

    public float Get_SFX_SliderValue()
    {
        return S_Slider.value;
    }
}
