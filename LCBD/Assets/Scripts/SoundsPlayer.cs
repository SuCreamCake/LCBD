using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

public class WalkSoundFile
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class JumpSoundFile
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class AttackSoundFile
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class LadderSoundFile
{
    public string SoundName;
    public AudioClip SoundClip;
}
[System.Serializable]
public class InteractionSoundFile
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

    [Header("< INTERACTION >")]
    [SerializeField] AudioSource InteractionSoundPlayer;
    [SerializeField] InteractionSoundFile[] InteractionSounds;

    void Start()
    {

    }
    void Update()
    {

    }

 

    public void WalkSound(int soundNum)
    {
        WalkSoundPlayer.clip = WalkSounds[soundNum].SoundClip;
        if ((Input.GetAxisRaw("Horizontal") != 0) && (Input.GetAxisRaw("Vertical") == 0) && (!JumpSoundPlayer.isPlaying))
        {
            if (!WalkSoundPlayer.isPlaying)
            {
                WalkSoundPlayer.Play();
                WalkSoundPlayer.loop = true;
            }
        }
        else
        {
            WalkSoundPlayer.Stop();
        }
    }
    public void JumpSound(int soundNum)
    {
        JumpSoundPlayer.clip = JumpSounds[soundNum].SoundClip;
        JumpSoundPlayer.Play();
    }
    public void AttackSound(int soundNum)
    {
        AttackSoundPlayer.clip = AttackSounds[soundNum].SoundClip;
        AttackSoundPlayer.Play();
    }
    public void LadderSound(int soundNum)
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
            LadderSoundPlayer.Stop();
    }
    public void InteractionSound(int soundNum)
    {
        InteractionSoundPlayer.clip = InteractionSounds[soundNum].SoundClip;
        InteractionSoundPlayer.Play();
    }
}