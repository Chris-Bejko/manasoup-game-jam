using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    AudioSource _audioSource;
    AudioSource _musicPlayer;
    [SerializeField]
    private List<IDToClip> _clips = new List<IDToClip>();  
    [System.Serializable]
    public class IDToClip
    {
        public AudioTrackID trackID;
        public AudioClip clip;
    }
    public void PlayAudio(AudioTrackID id)
    {
        foreach(var e in _clips)
        {
            if (e.trackID == id)
            {
                _audioSource.clip = e.clip;
                _audioSource.Play();
            }
        }
    }
}

public enum AudioTrackID
{
    CombatSFX,
    BackgroundMusic
}