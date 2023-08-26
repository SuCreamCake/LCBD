using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM_Toggle : MonoBehaviour
{
    private Toggle B_Toggle; //BGM≈‰±€
 
    public void Set_BGM_Toggle(bool isON)
    {
        if (isON == true)
            B_Toggle.isOn = true;
        else
            B_Toggle.isOn = false;
    }

    public bool Get_BGM_Toggle()
    {
        return B_Toggle;
    }
}
