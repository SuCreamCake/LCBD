using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Slider : MonoBehaviour
{
    private Slider B_Slider;


    public void Set_BGM_SliderValue(float value)
    {
        B_Slider.value = value;
        Debug.Log("°ª ¹Ù²î´ÂÁß");
    }

    public float Get_BGM_SliderValue()
    {
        return B_Slider.value;
    }
}
