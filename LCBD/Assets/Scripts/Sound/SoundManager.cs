using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundsPlayer SFX; //ȿ����
    [SerializeField] private Slider SFX_soundSlider; //SFX�����̴�
    [SerializeField] private SFX_Toggle SFX_toggle; //���Ұ� ���


    [SerializeField] private BGM BGM; //�����
    [SerializeField] private Slider BGM_soundSlider; //BGM�����̴�
    [SerializeField] private BGM_Toggle BGM_toggle;  //���Ұ� ���

    public AudioMixer Mixer; //���������� ������ ������ġ�� �����ϸ��.



    //[SerializeField] private Slider Master_soundSlider; //Master �����̴�
    //public Toggle Master_toggle;  //���Ұ� ���

    public static SoundManager instance = null; //����Ŵ����� DontDestroyOnLoad ����

    private void Awake()
    {
        DontDestroySoundManager();

        //BGM_soundSlider.onValueChanged.AddListener(SetBGMVolume);
        //SFX_soundSlider.onValueChanged.AddListener(SetSFXVolume);
        //Master_soundSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    

    public void DontDestroySoundManager() //DontDestroyOnLoad �̱�����������
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

    /*public void SetMasterVolume(float volume) //�ʿ����.
    {
        Mixer.SetFloat("Master_Param", Mathf.Log10(volume) * 20);
        if (volume <= 0.0001)
            Master_toggle.isOn = true;
        else
            Master_toggle.isOn = false;
    }*/


    public void SFX_IsMute(bool isOn)
    {
        if (SFX_toggle.Get_SFX_Toggle() == true) //����� isON�̸�
            SFX.SFX_Mute(isOn); //�Ҹ��� ����
        else
            SFX.SFX_Mute(isOn); //�Ҹ��� Ų��
    }

    public void BGM_IsMute(bool isOn)
    {
        if (BGM_toggle.Get_BGM_Toggle() == true) //����� isON�̸�
            BGM.BGM_Mute(isOn); //�Ҹ��� ����
        else
            BGM.BGM_Mute(isOn); //�Ҹ��� Ų��
    }

    /*public void Master_IsMute(float volume) //�ʿ����.
    {
        if (Master_toggle.isOn == true) //����� isON�̸�
            Mixer.SetFloat("Master_Param", 0);
        else
            Mixer.SetFloat("Master_Param", Mathf.Log10(volume) * 20);
    }*/


}
