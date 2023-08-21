using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class BGMSound : MonoBehaviour
{
    public Slider soundSlider; //�����̴�
    private float SoundValue; //���� ���� ���庯��
    private Toggle muteToggle; //���Ұ� ���

    public AudioSource musicSource; //�����
    public AudioMixer Mixer;
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        Mixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }
    
    public void OnMuteClieck(bool isOn) //���Ұ� �̺�Ʈ
    {
        if(isOn){
            SoundValue = soundSlider.value; //�����̴��� �Ҹ����� ����

            soundSlider.value = 0; //�����̴� ���� 0����
        }
        else //���Ұ� ������
        {
            soundSlider.value = SoundValue; //��� ������ ������ �ǵ�����
        }
    }
    /*
    public void OnMuteCheck() //���� Ȯ�� �� ���Ұ� Ȱ��ȭ
    {
        if(soundSlider.value==0 && !muteToggle.isOn)
        {
            muteToggle.isOn = true;
        }
        else if(soundSlider.value !=0 && muteToggle.isOn)
        {
            muteToggle.isOn = false;
        }
    }*/
}
