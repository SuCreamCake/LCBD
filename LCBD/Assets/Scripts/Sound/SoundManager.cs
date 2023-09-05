using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundsPlayer SFX; //ȿ����
    private Toggle sfx_Toggle; //ȿ���� ���Ұ� ���
    private Slider SFX_Slider;
    private float SFX_SaveVolume;

    private static float BGMSoudValue;

    public BGM BGM; //�����
    private Toggle bgm_Toggle; //����� ���Ұ� ���
    private Slider BGM_Slider;
    private float BGM_SaveVolume;


    public AudioMixer Mixer; //���������� ������ ������ġ�� �����ϸ��.

    private void Awake()
    {

        //BGM_soundSlider.onValueChanged.AddListener(SetBGMVolume);
        //SFX_soundSlider.onValueChanged.AddListener(SetSFXVolume);
        //Master_soundSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    public void Set_BGM_Volume(float Volume) //BGM���� �����Լ�
    {
        BGM.BGM_Volume(Volume);
        BGMSoudValue = Volume;
        BGM.BGM_Volume(BGMSoudValue);
    }

    public void Set_SFX_Volume(float Volume) //SFX���� �����Լ�
    {
        Mixer.SetFloat("SFX_Param", Mathf.Log10(Volume) * 20);
    }


    public void SFX_IsMute(bool isOn)
    {
        if (isOn == true) //����� isON�̸�
        {
            SFX.SFX_Mute(true);
        }
        else
            SFX.SFX_Mute(isOn); //�Ҹ��� Ų��
    }

    public void BGM_IsMute(bool isOn)
    {
        if (isOn == true) //����� isON�̸�
        {
            BGM.BGM_Mute(true);
        }
        else
        {
            BGM.BGM_Mute(false);
        }
    }


}
