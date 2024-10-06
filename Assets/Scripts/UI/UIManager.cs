using UnityEngine;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private AudioMixer audioMixer; 
    private const string SFXVolumeParam = "sfx"; 

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
                settingsScreen.SetActive(false);
            }
            else
                PauseGame(true);
        }
    }

    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        if (status)
        {
            Time.timeScale = 0;
            MuteSFX(true);
        }
        else
        {
            Time.timeScale = 1;
            MuteSFX(false);
        }
    }

    public void UnpauseGame() 
    {
        PauseGame(false);
    }

    private void MuteSFX(bool mute)
    {
      /*  float volume = mute ? -80f : 0f; 
        audioMixer.SetFloat(SFXVolumeParam, volume);*/
    }
}
