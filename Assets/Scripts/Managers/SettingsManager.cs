using Manasoup;
using Manasoup.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [SerializeField]
    Slider SFXSlider;

    [SerializeField]
    Slider MusicSlider;

    [SerializeField]
    AudioSource MusicPlayer;

    // Start is called before the first frame update
    void Start()
    {
        InitSliders();
    }

    private void InitSliders()
    {
        var hasPlayed = PlayerPrefs.GetInt("HasPlayed") == 1;
        if (hasPlayed)
        {
            var sfx = PlayerPrefs.GetFloat("SFX");
            var music = PlayerPrefs.GetFloat("Music");
            SFXSlider.SetValueWithoutNotify(sfx);
            MusicSlider.SetValueWithoutNotify(music);
            GameManager.Instance.uIManager.SetUIVolume(PlayerPrefs.GetFloat("SFX"));
            SFXSlider.onValueChanged?.Invoke(sfx);
            MusicSlider.onValueChanged?.Invoke(music);
        }
        else
        {
            PlayerPrefs.SetInt("HasPlayed", 1);
            PlayerPrefs.SetInt("SFX", 1);
            PlayerPrefs.SetInt("Music", 1);
        }

        SFXSlider.onValueChanged.AddListener(GameManager.Instance.uIManager.SetUIVolume);
        SFXSlider.onValueChanged.AddListener(GameManager.Instance.player.GetComponent<CharacterCombat>().SetVolume);
        SFXSlider.onValueChanged.AddListener(GameManager.Instance.player.SetVolume);
        SFXSlider.onValueChanged.AddListener(GameManager.Instance.enemiesManager.SetVolume);
        MusicSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    void SetMusicVolume(float volume)
    {
        MusicPlayer.volume = volume;
    }
}
