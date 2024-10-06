using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        float musicVolume;
        float sfxVolume;
        
        myMixer.GetFloat("music", out musicVolume);
        myMixer.GetFloat("sfx", out sfxVolume);

        musicSlider.value = Mathf.Pow(10, musicVolume / 20);
        sfxSlider.value = Mathf.Pow(10, sfxVolume / 20);
    }

    public void SetMusicVolume()
    {
        float musicVolume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(musicVolume) * 20);
    }

    public void SetSFXVolume()
    {
        float sfxVolume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(sfxVolume) * 20);
    }
}
