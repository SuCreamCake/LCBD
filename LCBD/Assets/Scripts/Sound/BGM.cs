using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGM : MonoBehaviour
{
    public AudioSource musicSource; //����� ������ҽ�
    public AudioMixer Mixer; //�ش� ������� �ͼ�


    public void BGM_Mute(bool isMute)
    {
        musicSource.mute = isMute;
    }
}
