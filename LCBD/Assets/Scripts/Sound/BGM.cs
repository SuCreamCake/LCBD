using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour
{
    [System.Serializable]
    public struct BGMType
    {
        public string Stagename; //오디오 이름
        public AudioClip audio; //오디오클립
    }

    public BGMType[] BGMList;
    private AudioSource musicSource; //배경음 오디오소스
    public AudioMixer Mixer; //해당 오디오의 믹서


    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for(int i=0; i<BGMList.Length; i++)
        {
            if(arg0.name == BGMList[i].Stagename)
            {
                musicSource.clip = BGMList[i].audio;
                musicSource.volume = 0.5f;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
    }

    public void BGM_Mute(bool isMute)
    {
        musicSource.mute = isMute;
    }

    public void BGM_Volume(float value)
    {
        musicSource.volume = value;
        Mixer.SetFloat("BGM_Param", Mathf.Log10(value) * 20);
    }
}
