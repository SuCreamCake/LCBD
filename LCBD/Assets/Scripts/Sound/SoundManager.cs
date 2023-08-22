using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundsPlayer SFX; //ȿ����
    [SerializeField] private Slider SFX_soundSlider; //SFX�����̴�


    [SerializeField] private BGM BGM; //�����
    [SerializeField] private Slider BGM_soundSlider; //SFX�����̴�

    [SerializeField] private Slider Master_soundSlider; //SFX�����̴�
    public AudioMixer Mixer; //���������� ������ ������ġ�� �����ϸ��.


    private float SoundValue; //���� ���� ���庯��
    private Toggle MuteToggle; //���Ұ� ���


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

    public void OnMuteClieck(bool isOn) //���Ұ� �̺�Ʈ
    {
        MuteToggle.isOn = true;
    }


}
