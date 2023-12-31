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

    #region UpdateMusicState Kommentare
    //Wenn die Musik aktiviert ist, wird die Musik abgespielt und der Text gelb 
    //Wenn die Musik deaktiviert ist, wird die Musik pausiert und der Text weiﬂ
    #endregion
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

    #region ToggleMusic Kommentare
    //Aktiviert oder deaktiviert die Musik
    #endregion
    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;
        UpdateMusicState();
    }
}
