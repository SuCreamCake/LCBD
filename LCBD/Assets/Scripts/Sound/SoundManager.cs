using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundsPlayer SFX; //효과음
    [SerializeField] private Slider SFX_soundSlider; //SFX슬라이더
    public Toggle SFX_toggle; //음소거 토글


    [SerializeField] private BGM BGM; //배경음
    [SerializeField] private Slider BGM_soundSlider; //BGM슬라이더
    public Toggle BGM_toggle;  //음소거 토글

    [SerializeField] private Slider Master_soundSlider; //Master 슬라이더
    public AudioMixer Mixer; //최종적으로 나오는 사운드장치라 생각하면됨.
    public Toggle Master_toggle;  //음소거 토글

   // public static SoundManager instance = null;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        /*if (instance == null)
        { //오브젝트가 없는경우 기존에 있는 오브젝트를 넣는다.
            instance = this;
            Debug.Log("인스턴스 넣어줌!");
        }
        else if (instance != this)
        { //이미 인스턴스가 있으면 오브젝트 제거
            Destroy(gameObject);
            Debug.Log("생성된 인스턴스 삭제!");
        }

        DontDestroyOnLoad(gameObject);*/

        BGM_soundSlider.onValueChanged.AddListener(SetBGMVolume);
        SFX_soundSlider.onValueChanged.AddListener(SetSFXVolume);
        Master_soundSlider.onValueChanged.AddListener(SetMasterVolume);

    }

    private void SetSFXVolume(float volume)
    {
        //SFX. = volume;
        Mixer.SetFloat("SFX_Param", Mathf.Log10(volume) * 20);
        if (volume <= 0.0001)
            SFX_toggle.isOn = true;
        else
            SFX_toggle.isOn = false;
    }

    private void SetBGMVolume(float volume)
    {
        Mixer.SetFloat("BGM_Param", Mathf.Log10(volume) * 20);
        if (volume <= 0.0001)
            BGM_toggle.isOn = true;
        else
            BGM_toggle.isOn = false;
    }

    private void SetMasterVolume(float volume)
    {
        Mixer.SetFloat("Master_Param", Mathf.Log10(volume) * 20);
        if (volume <= 0.0001)
            Master_toggle.isOn = true;
        else
            Master_toggle.isOn = false;
    }


    public void SFX_IsMute()
    {
        if (SFX_toggle.isOn == true) //토글이 isON이면
            SFX.SFX_Mute(true); //소리를 끈다
        else
            SFX.SFX_Mute(false); //소리를 킨다
    }

    public void BGM_IsMute()
    {
        if (BGM_toggle.isOn == true) //토글이 isON이면
            BGM.BGM_Mute(true); //소리를 끈다
        else
            BGM.BGM_Mute(false); //소리를 킨다
    }

    private void Master_IsMute(float volume)
    {
        if (Master_toggle.isOn == true) //토글이 isON이면
            Mixer.SetFloat("Master_Param", 0);
        else
            Mixer.SetFloat("Master_Param", Mathf.Log10(volume) * 20);
    }


}
