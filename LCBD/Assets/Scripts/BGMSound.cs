using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class BGMSound : MonoBehaviour
{
    public Slider soundSlider; //슬라이더
    private float SoundValue; //이전 사운드 저장변수
    private Toggle muteToggle; //음소거 토글

    public AudioSource musicSource; //배경음
    public AudioMixer Mixer;
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        Mixer.SetFloat("Music", Mathf.Log10(volume)*20);
    }
    
    public void OnMuteClieck(bool isOn) //음소거 이벤트
    {
        if(isOn){
            SoundValue = soundSlider.value; //슬라이더의 소리값을 저장

            soundSlider.value = 0; //슬라이더 값은 0으로
        }
        else //음소거 해제시
        {
            soundSlider.value = SoundValue; //취소 이전의 값으로 되돌리기
        }
    }
    /*
    public void OnMuteCheck() //음량 확인 후 음소거 활성화
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
