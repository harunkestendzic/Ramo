using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] musicClips; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (musicClips.Length > 0)
        {
            int randomIndex = Random.Range(0, musicClips.Length);
            audioSource.clip = musicClips[randomIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No music clips assigned in the MusicPlayer script.");
        }
    }
}
