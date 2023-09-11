using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundsPlayer SFX; //효과음
    private Toggle sfx_Toggle; //효과음 음소거 토글
    private Slider SFX_Slider;
    private float SFX_SaveVolume;

    private static float BGMSoudValue;

    public BGM BGM; //배경음
    private Toggle bgm_Toggle; //배경음 음소거 토글
    private Slider BGM_Slider;
    private float BGM_SaveVolume;


    public AudioMixer Mixer; //최종적으로 나오는 사운드장치라 생각하면됨.

    private void Awake()
    {

        //BGM_soundSlider.onValueChanged.AddListener(SetBGMVolume);
        //SFX_soundSlider.onValueChanged.AddListener(SetSFXVolume);
        //Master_soundSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    public void Set_BGM_Volume(float Volume) //BGM볼륨 조절함수
    {
        BGM.BGM_Volume(Volume);
        BGMSoudValue = Volume;
        BGM.BGM_Volume(BGMSoudValue);
    }

    public void Set_SFX_Volume(float Volume) //SFX볼륨 조절함수
    {
        Mixer.SetFloat("SFX_Param", Mathf.Log10(Volume) * 20);
    }


    public void SFX_IsMute(bool isOn)
    {
        if (isOn == true) //토글이 isON이면
        {
            SFX.SFX_Mute(true);
        }
        else
            SFX.SFX_Mute(isOn); //소리를 킨다
    }

    public void BGM_IsMute(bool isOn)
    {
        if (isOn == true) //토글이 isON이면
        {
            BGM.BGM_Mute(true);
        }
        else
        {
            BGM.BGM_Mute(false);
        }
    }


}
