using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public TextMeshProUGUI musicText;

    private bool musicEnabled = true;

    private void OnEnable()
    {
        ToggleMusic();
    }
    private void OnDisable()
    {
        ToggleMusic();
    }

    #region ToggleMusic Kommentare
    //Schaltet die Musik an oder aus
    //Wenn die Musik an ist, wird sie pausiert und der ButtonText wird weiﬂ.
    //Wenn die Musik aus ist, wird sie abgespielt und der ButtonText wird gelb.
    #endregion
    public void ToggleMusic()
    {
        if(musicEnabled)
        {
            musicAudioSource.Pause();
            musicText.color = Color.white;
            musicEnabled = false;

        }
        else
        {
            if(!musicAudioSource.isPlaying)
            {
                musicAudioSource.Play();
            }
            else
            {
                musicAudioSource.UnPause();
            }
            musicText.color = Color.yellow;
            musicEnabled = true;
        }
    }
}
