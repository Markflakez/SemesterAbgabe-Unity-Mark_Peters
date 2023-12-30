using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public TextMeshProUGUI musicText;

    private bool musicEnabled = false;

    private void Start()
    {
        UpdateMusicState();
    }

    private void OnEnable()
    {
        UpdateMusicState();
    }


    private void UpdateMusicState()
    {
        if (musicEnabled)
        {
            musicAudioSource.Play();
            musicText.color = Color.yellow;
        }
        else
        {
            musicAudioSource.Pause();
            musicText.color = Color.white;
        }
    }

    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        UpdateMusicState();
    }
}
