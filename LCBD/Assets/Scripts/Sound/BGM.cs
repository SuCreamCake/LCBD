using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class BGM : MonoBehaviour
{
    public AudioSource musicSource; //배경음 오디오소스
    public AudioMixer Mixer; //해당 오디오의 믹서


    public void BGM_Mute(bool isMute)
    {
        musicSource.mute = isMute;
    }
}
