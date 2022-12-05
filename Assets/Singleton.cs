using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{

    [SerializeField]
    AudioSource MusicPlayer;

    public static Singleton Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetVolume(float volume)
    {
        MusicPlayer.volume = volume;
    }
}
