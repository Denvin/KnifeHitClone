using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region SingleTon
    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //music.volume = PlayerPrefs.GetFloat(PREFS_MUSIC_VOLUME, 0.5f);
            //effect.volume = PlayerPrefs.GetFloat(PREFS_EFFECT_VOLUME, 0.5f);
        }

    }
    #endregion

    [SerializeField] AudioSource effectSource;

    public void PlaySound(AudioClip audio)
    {
        effectSource.PlayOneShot(audio);
    }
}
