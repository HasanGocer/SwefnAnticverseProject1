using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] private AudioSource musicSource, effectSource;
    [SerializeField] private AudioClip mainMusic, bloomEffect, goldEffect;

    public void MainMusicPlay()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
        SetMusicVolume();
        SetSoundVolume();
    }
    public void SetMusicVolume()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.music == 1)
            musicSource.volume = 0;
        else
            musicSource.volume = 1;
    }

    public void SetSoundVolume()
    {
        GameManager gameManager = GameManager.Instance;

        if (gameManager.sound == 1)
            effectSource.volume = 0;
        else
            effectSource.volume = 1;
    }

    public void EffectCall()
    {
        musicSource.PlayOneShot(bloomEffect);
    }
    public void EffectGoldCall()
    {
        musicSource.PlayOneShot(goldEffect);
    }
}
