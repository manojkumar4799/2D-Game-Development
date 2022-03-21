using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager soundManager;
    [SerializeField] SetSounds[] setsound;
    public AudioSource BGM;
    public AudioSource soundSFX;
    private void Awake()
    {
        if (soundManager == null)
        {
            soundManager = this;
            DontDestroyOnLoad(soundManager);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        BackGroundMusic(Sound.GameMusic);
    }

    public static SoundManager Instance
    {
        get { return soundManager; }
    }

    public void PlayerFootSteps(Sound footSteps, bool play)
    {
        AudioClip clip = GetSoundClip(footSteps);
        soundSFX.clip = clip;
        
        if (play)
        {
            soundSFX.Play();
        }
        else
        {
            soundSFX.Stop();
        }
    }

    public void PlaySound( Sound sound)
    {
        AudioClip clip = GetSoundClip(sound);
        if (clip != null)
        {
            soundSFX.PlayOneShot(clip);
        }
    }
    public void BackGroundMusic(Sound music)
    {
        if (music == Sound.GameMusic||music==Sound.BossMusic)
        {
            BGM.loop = true;
        }
        else
        {
            BGM.loop = false;
        }
        AudioClip clip = GetSoundClip(music);
        BGM.clip = clip;
        BGM.Play();
    }

    private AudioClip GetSoundClip(Sound sound)
    {
        SetSounds matchSound = Array.Find(setsound, obj => obj.soundType == sound);
            if (matchSound != null)
            {
              return matchSound.SFX;
            }
            else return null;
    }
}


[Serializable]
public class SetSounds
{
    public AudioClip SFX;
    public Sound soundType;
}


public enum Sound
{
    ButtonClick,
    PlayerDeath,
    PlayerMove,
    EnemyDeath,
    jump,
    GameMusic,
    StartLevel,
    levelComplete,
    BossMusic

}
