using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFX_Toggle : MonoBehaviour
{
    private Toggle S_Toggle; //BGM≈‰±€

    public void Set_SFX_Toggle(bool isON)
    {
        if (isON == true)
            S_Toggle.isOn = true;
        else
            S_Toggle.isOn = false;
    }

    public bool Get_SFX_Toggle()
    {
        return S_Toggle;
    }
}
