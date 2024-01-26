using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class WalkSoundFile          // �ȱ� & �ٱ�
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class JumpSoundFile          // ����
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class AttackSoundFile        // ����
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class LadderSoundFile        // ��ٸ�
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class ItemSoundFile          // ��� & �Ҹ�ǰ ���
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class InteractionSoundFile   // ��Ż & @
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class UISoundFile            // ��ư & @
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class GimmickSoundFile01            // ���01
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class GimmickSoundFile02            // ���02
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

    [Header("< Gimmick01 >")]
    [SerializeField] AudioSource Gimmick01SoundPlayer;
    [SerializeField] UISoundFile[] Gimmick01Sounds;

    [Header("< Gimmick02 >")]
    [SerializeField] AudioSource Gimmick02SoundPlayer;
    [SerializeField] UISoundFile[] Gimmick02Sounds;

    public AudioMixer Mixer; //�ش� ������� �ͼ�
    private float saveValue;

    ///////////////////////////////////////////�Լ� ĿƮ����////////////////////////////


    void Start()
    {
        
    }
    void Update()
    {

    }

    public void SFX_Mute(bool isMute) //ȿ���� ��ü ���
    {
        WalkSoundPlayer.mute = isMute;
        JumpSoundPlayer.mute = isMute;
        AttackSoundPlayer.mute = isMute;
        LadderSoundPlayer.mute = isMute;
        InteractionSoundPlayer.mute = isMute;
        UISoundPlayer.mute = isMute;
        Gimmick01SoundPlayer.mute = isMute;
        Gimmick02SoundPlayer.mute = isMute;
    }
    public void SFX_Volume(float value)
    {
        saveValue = value;
        Mixer.SetFloat("SFX_Param", Mathf.Log10(value) * 20); //�����̴����� �ҷ��ͼ� ��Ʈ��
    }

 
    public void WalkSound(int soundNum)                                                 // USE
    {
            
        WalkSoundPlayer.clip = WalkSounds[soundNum].SoundClip;                          // �Ƚ�Ʈ������Ʈ ���ָ� ������ �ȵǴ��� �ɵ�?
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
            WalkSoundPlayer.volume = 0.4f;  // �ȱ� �Ҹ� ũ��
        else
            WalkSoundPlayer.volume = 0.8f;  // �ٱ� �Ҹ� ũ��

    }

    public void JumpSound(int soundNum)                                                 // USE
    {
        JumpSoundPlayer.clip = JumpSounds[soundNum].SoundClip;
        JumpSoundPlayer.Play();
    }
    public void AttackSound(int soundNum)                                               // USE
    {
        AttackSoundPlayer.clip = AttackSounds[soundNum].SoundClip;
        AttackSoundPlayer.Play();
    }

    // ��ٸ� ���ϸ��̼��� ��ٸ� Ÿ�� ���� ��� Ȱ��ȭ�Ǿ� �Ҹ��� ��� �� -> �ذ�
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
        LadderSoundPlayer.Stop();                                                       // Ű�ٿ�,�� ���� �ٲ���� �������� �Է�Ű �ٲ㵵 �Ҹ��� ��.
    }                                                                                   // ���� �Է�Ű �ٲٸ� �Ҹ��� �ȳ�

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
    public void UISound(int soundNum)                                                   // X_�Ƹ� ��ư ���忡 ���� ��
    {
        UISoundPlayer.clip = UISounds[soundNum].SoundClip;
        UISoundPlayer.Play();
    }
    public void Gimmick01Sound(int soundNum)
    {
        Gimmick01SoundPlayer.clip = Gimmick01Sounds[soundNum].SoundClip;
        Gimmick01SoundPlayer.Play();
    }
    public void Gimmick02Sound(int soundNum)
    {
        Gimmick02SoundPlayer.clip = Gimmick02Sounds[soundNum].SoundClip;
        Gimmick02SoundPlayer.Play();
    }
}