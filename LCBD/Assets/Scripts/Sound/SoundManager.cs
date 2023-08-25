using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{
    [SerializeField]private SoundsPlayer SFX; //ȿ����
    [SerializeField] private Slider SFX_soundSlider; //SFX�����̴�
    public Toggle SFX_toggle; //���Ұ� ���


    [SerializeField] private BGM BGM; //�����
    [SerializeField] private Slider BGM_soundSlider; //BGM�����̴�
    public Toggle BGM_toggle;  //���Ұ� ���

    [SerializeField] private Slider Master_soundSlider; //Master �����̴�
    public AudioMixer Mixer; //���������� ������ ������ġ�� �����ϸ��.
    public Toggle Master_toggle;  //���Ұ� ���

   // public static SoundManager instance = null;

    private void Awake()
    {
        //DontDestroyOnLoad(this);
        /*if (instance == null)
        { //������Ʈ�� ���°�� ������ �ִ� ������Ʈ�� �ִ´�.
            instance = this;
            Debug.Log("�ν��Ͻ� �־���!");
        }
        else if (instance != this)
        { //�̹� �ν��Ͻ��� ������ ������Ʈ ����
            Destroy(gameObject);
            Debug.Log("������ �ν��Ͻ� ����!");
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
        if (SFX_toggle.isOn == true) //����� isON�̸�
            SFX.SFX_Mute(true); //�Ҹ��� ����
        else
            SFX.SFX_Mute(false); //�Ҹ��� Ų��
    }

    public void BGM_IsMute()
    {
        if (BGM_toggle.isOn == true) //����� isON�̸�
            BGM.BGM_Mute(true); //�Ҹ��� ����
        else
            BGM.BGM_Mute(false); //�Ҹ��� Ų��
    }

    private void Master_IsMute(float volume)
    {
        if (Master_toggle.isOn == true) //����� isON�̸�
            Mixer.SetFloat("Master_Param", 0);
        else
            Mixer.SetFloat("Master_Param", Mathf.Log10(volume) * 20);
    }


}
