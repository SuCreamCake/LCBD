using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundsPlayer SFX; //효과음
    [SerializeField] private Slider SFX_soundSlider; //SFX슬라이더


    [SerializeField] private BGM BGM; //배경음
    [SerializeField] private Slider BGM_soundSlider; //SFX슬라이더

    [SerializeField] private Slider Master_soundSlider; //SFX슬라이더
    public AudioMixer Mixer; //최종적으로 나오는 사운드장치라 생각하면됨.


    private float SoundValue; //이전 사운드 저장변수
    private Toggle MuteToggle; //음소거 토글


    private void Awake()
    {
        DontDestroyOnLoad(this);

        BGM_soundSlider.onValueChanged.AddListener(SetBGMVolume);
        SFX_soundSlider.onValueChanged.AddListener(SetSFXVolume);
        Master_soundSlider.onValueChanged.AddListener(SetMasterVolume);

    }

    public void SetSFXVolume(float volume)
    {
        //SFX. = volume;
        Mixer.SetFloat("SFX_Param", Mathf.Log10(volume) * 20);
        if (volume <= 0.0001)
            MuteToggle.isOn = true;
        else
            MuteToggle.isOn = false;
    }

    public void SetBGMVolume(float volume)
    {
        Mixer.SetFloat("BGM_Param", Mathf.Log10(volume) * 20);
        if (volume <= 0.0001)
            MuteToggle.isOn = true;
        else
            MuteToggle.isOn = false;
    }

    public void SetMasterVolume(float volume)
    {
        Mixer.SetFloat("Master_Param", Mathf.Log10(volume) * 20);
        if (volume <= 0.0001)
            MuteToggle.isOn = true;
        else
            MuteToggle.isOn = false;
    }

    public void OnMuteClieck(bool isOn) //음소거 이벤트
    {
        MuteToggle.isOn = true;
    }


}
