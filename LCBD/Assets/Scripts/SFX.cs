using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{

    AudioSource AudioSource;
    public AudioClip SoundJump;
    public AudioClip SoundAttack;
    public AudioClip SoundWalk;

    List<AudioClip> SoundClips;
    
    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();

        SoundClips = new List<AudioClip>();
    }

    void SoundValue()
    {

    }

    public void SoundPlay(string action)
    {
        switch (action)
        {
            case "Attack":
                AudioSource.clip = SoundAttack;
                break;
            case "Walk":
                AudioSource.clip = SoundWalk;
                break;
            case "Jump":
                AudioSource.clip = SoundJump;
                break;
        }
        AudioSource.Play();
    }



}
