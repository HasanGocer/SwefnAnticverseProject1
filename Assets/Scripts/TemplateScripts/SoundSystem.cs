using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSystem : MonoSingleton<SoundSystem>
{
    [SerializeField] private AudioSource musicSource, effectSource;
    [SerializeField] private Slider musicSlider, effectSlider;
    [SerializeField] private AudioClip mainMusic, bloomEffect, goldEffect;

    public void MainMusicPlay()
    {
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    public void EffectCall()
    {
        musicSource.PlayOneShot(bloomEffect);
    }
    public void EffectGoldCall()
    {
        musicSource.PlayOneShot(goldEffect);
    }
    private void Update()
    {
        musicSource.volume = musicSlider.value;
        effectSource.volume = effectSlider.value;
    }
}
