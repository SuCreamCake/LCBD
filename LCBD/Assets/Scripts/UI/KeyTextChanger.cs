using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyTextChanger : MonoBehaviour
{
    public Text[] txt; //텍스트

    void Start()
    {
        for(int i=0;i<txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(KeyInput)i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        KeyTextChage(); //키 텍스트 바뀌는 함수
    }

    private void KeyTextChage()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(KeyInput)i].ToString(); //입력한 키로 바뀌는 함수
        }
    }

}
