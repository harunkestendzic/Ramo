using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }

    public void PlayLoopingSound(AudioClip _sound)
    {
        if (_sound == null || (source.isPlaying && source.clip == _sound)) return;

        source.clip = _sound;
        source.loop = true;
        source.Play();
    }

    public void StopLoopingSound()
    {
        if (source.isPlaying && source.loop)
        {
            source.loop = false;
            source.Stop();
        }
    }

    public bool IsPlaying(AudioClip _sound)
    {
        return source.isPlaying && source.clip == _sound;
    }
}
