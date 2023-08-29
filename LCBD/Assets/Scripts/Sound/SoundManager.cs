using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundsPlayer SFX; //효과음
    [SerializeField] private Slider SFX_soundSlider; //SFX슬라이더
    [SerializeField] private SFX_Toggle SFX_toggle; //음소거 토글


    [SerializeField] private BGM BGM; //배경음
    [SerializeField] private Slider BGM_soundSlider; //BGM슬라이더
    [SerializeField] private BGM_Toggle BGM_toggle;  //음소거 토글

    public AudioMixer Mixer; //최종적으로 나오는 사운드장치라 생각하면됨.



    //[SerializeField] private Slider Master_soundSlider; //Master 슬라이더
    //public Toggle Master_toggle;  //음소거 토글

    public static SoundManager instance = null; //사운드매니저의 DontDestroyOnLoad 변수

    private void Awake()
    {
        DontDestroySoundManager();

        //BGM_soundSlider.onValueChanged.AddListener(SetBGMVolume);
        //SFX_soundSlider.onValueChanged.AddListener(SetSFXVolume);
        //Master_soundSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    

    public void DontDestroySoundManager() //DontDestroyOnLoad 싱글톤패턴으로
    {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }


    public void SetSFXVolume(float value)
    {
        SFX.SFX_Volume(value);
        //Mixer.SetFloat("SFX_Param", Mathf.Log10(value) * 20);
    }


    public void SetBGMVolume(float value)
    {
        BGM.BGM_Volume(value);
        //Mixer.SetFloat("BGM_Param", Mathf.Log10(value) * 20);
    }

    /*public void SetMasterVolume(float volume) //필요없음.
    {
        Mixer.SetFloat("Master_Param", Mathf.Log10(volume) * 20);
        if (volume <= 0.0001)
            Master_toggle.isOn = true;
        else
            Master_toggle.isOn = false;
    }*/


    public void SFX_IsMute(bool isOn)
    {
        if (SFX_toggle.Get_SFX_Toggle() == true) //토글이 isON이면
            SFX.SFX_Mute(isOn); //소리를 끈다
        else
            SFX.SFX_Mute(isOn); //소리를 킨다
    }

    public void BGM_IsMute(bool isOn)
    {
        if (BGM_toggle.Get_BGM_Toggle() == true) //토글이 isON이면
            BGM.BGM_Mute(isOn); //소리를 끈다
        else
            BGM.BGM_Mute(isOn); //소리를 킨다
    }

    /*public void Master_IsMute(float volume) //필요없음.
    {
        if (Master_toggle.isOn == true) //토글이 isON이면
            Mixer.SetFloat("Master_Param", 0);
        else
            Mixer.SetFloat("Master_Param", Mathf.Log10(volume) * 20);
    }*/


}
