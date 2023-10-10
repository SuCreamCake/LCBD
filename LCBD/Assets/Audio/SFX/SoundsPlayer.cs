using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class WalkSoundFile          // 걷기 & 뛰기
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class JumpSoundFile          // 점프
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class AttackSoundFile        // 공격
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class LadderSoundFile        // 사다리
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class ItemSoundFile          // 장비 & 소모품 등등
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class InteractionSoundFile   // 포탈 & @
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class UISoundFile            // 버튼 & @
{
    public string SoundName;
    public AudioClip SoundClip;
}

public class SoundsPlayer : MonoBehaviour
{

    [Header("< WALK >")]
    [SerializeField] AudioSource WalkSoundPlayer;
    [SerializeField] WalkSoundFile[] WalkSounds;

    [Header("< JUMP >")]
    [SerializeField] AudioSource JumpSoundPlayer;
    [SerializeField] JumpSoundFile[] JumpSounds;

    [Header("< ATTACK >")]
    [SerializeField] AudioSource AttackSoundPlayer;
    [SerializeField] AttackSoundFile[] AttackSounds;

    [Header("< LADDER >")]
    [SerializeField] AudioSource LadderSoundPlayer;
    [SerializeField] LadderSoundFile[] LadderSounds;

    [Header("< Item >")]
    [SerializeField] AudioSource ItemSoundPlayer;
    [SerializeField] ItemSoundFile[] ItemSounds;

    [Header("< INTERACTION >")]
    [SerializeField] AudioSource InteractionSoundPlayer;
    [SerializeField] InteractionSoundFile[] InteractionSounds;

    [Header("< UI >")]
    [SerializeField] AudioSource UISoundPlayer;
    [SerializeField] UISoundFile[] UISounds;

    public AudioMixer Mixer; //해당 오디오의 믹서
    private float saveValue;

    ///////////////////////////////////////////함수 커트라인////////////////////////////


    void Start()
    {
        
    }
    void Update()
    {

    }

    public void SFX_Mute(bool isMute) //효과음 전체 통괄
    {
        WalkSoundPlayer.mute = isMute;
        JumpSoundPlayer.mute = isMute;
        AttackSoundPlayer.mute = isMute;
        LadderSoundPlayer.mute = isMute;
        InteractionSoundPlayer.mute = isMute;
    }
    public void SFX_Volume(float value)
    {
        saveValue = value;
        Mixer.SetFloat("SFX_Param", Mathf.Log10(value) * 20); //슬라이더값을 불러와서 컨트롤
    }

 

    public void WalkSound(int soundNum)                                                 // USE
    {
            
        WalkSoundPlayer.clip = WalkSounds[soundNum].SoundClip;                          // 픽스트업데이트 없애면 기존에 안되던가 될듯?
        if ((Input.GetAxisRaw("Horizontal") != 0) && (Input.GetAxisRaw("Vertical") == 0) && (!JumpSoundPlayer.isPlaying) && (!LadderSoundPlayer.isPlaying))
        {
            if (!WalkSoundPlayer.isPlaying)
            {
                WalkSoundPlayer.Play();
                WalkSoundPlayer.loop = true;
            }
        }
        else
        {
            WalkSoundPlayer.loop = false;
            WalkSoundPlayer.Stop();
            WalkSoundPlayer.clip = null;

        }
        if (soundNum == 0)
            WalkSoundPlayer.volume = 0.4f;  // 걷기 소리 크기
        else
            WalkSoundPlayer.volume = 0.8f;  // 뛰기 소리 크기

    }

    public void JumpSound(int soundNum)                                                 // USE
    {
        JumpSoundPlayer.clip = JumpSounds[soundNum].SoundClip;
        JumpSoundPlayer.Play();
    }
    public void AttackSound(int soundNum)                                               // X
    {
        AttackSoundPlayer.clip = AttackSounds[soundNum].SoundClip;
        AttackSoundPlayer.Play();
    }

    // 사다리 에니메이션이 사다리 타는 순간 계속 활성화되어 소리도 계속 남 -> 해결
    public void LadderSound(int soundNum)                                               // USE
    {
        LadderSoundPlayer.clip = LadderSounds[soundNum].SoundClip;
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (!LadderSoundPlayer.isPlaying)
            {
                LadderSoundPlayer.Play();
                LadderSoundPlayer.loop = true;
            }
        }
        else
        {
            LadderSoundPlayer.Stop();
        }
            
    }
    public void LadderSoundStop()                                                       // USE
    {
        LadderSoundPlayer.Stop();                                                       // 키다운,업 으로 바꿔야지 설정에서 입력키 바꿔도 소리가 남.
    }                                                                                   // 현재 입력키 바꾸면 소리가 안남
    public void ItemSound(int soundNum)                                                 // X
    {
        ItemSoundPlayer.clip = ItemSounds[soundNum].SoundClip;
        ItemSoundPlayer.Play();
    }
    public void InteractionSound(int soundNum)                                          // USE
    {
        InteractionSoundPlayer.clip = InteractionSounds[soundNum].SoundClip;
        InteractionSoundPlayer.Play();
    }
    public void UISound(int soundNum)                                                   // X
    {
        UISoundPlayer.clip = UISounds[soundNum].SoundClip;
        UISoundPlayer.Play();
    }
}