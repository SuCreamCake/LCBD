using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGM : MonoBehaviour
{
    public AudioSource musicSource; //����� ������ҽ�
    public AudioMixer Mixer; //�ش� ������� �ͼ�
    private float saveValue;

    public void BGM_Mute(bool isMute)
    {
        musicSource.mute = isMute;
    }

    public void BGM_Volume(float value)
    {
        saveValue = value;
        Mixer.SetFloat("BGM_Param", Mathf.Log10(value) * 20);
    }
}
