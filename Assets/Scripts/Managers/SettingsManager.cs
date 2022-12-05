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


    // Start is called before the first frame update
    void Start()
    {
        InitSliders();
    }

    private void InitSliders()
    {
        var hasPlayed = PlayerPrefs.GetInt("HasPlayed") == 1;
        var sfx = 1f;
        var music = 1f;
        if (hasPlayed)
        {
            sfx = PlayerPrefs.GetFloat("SFX");
            music = PlayerPrefs.GetFloat("Music");
            SFXSlider.SetValueWithoutNotify(sfx);
            MusicSlider.SetValueWithoutNotify(music);
            GameManager.Instance.uIManager.SetUIVolume(PlayerPrefs.GetFloat("SFX"));
            SFXSlider.onValueChanged?.Invoke(sfx);
            MusicSlider.onValueChanged?.Invoke(music);
        }
        else
        {
            PlayerPrefs.SetInt("HasPlayed", 1);
            PlayerPrefs.SetFloat("SFX", 1);
            PlayerPrefs.SetFloat("Music", 1);
        }
        SFXSlider.SetValueWithoutNotify(sfx);
        MusicSlider.SetValueWithoutNotify(music);
        GameManager.Instance.uIManager.SetUIVolume(PlayerPrefs.GetFloat("SFX"));
        SFXSlider.onValueChanged?.Invoke(sfx);
        MusicSlider.onValueChanged?.Invoke(music);
        SFXSlider.onValueChanged.AddListener(GameManager.Instance.uIManager.SetUIVolume);
        SFXSlider.onValueChanged.AddListener(GameManager.Instance.player.GetComponent<CharacterCombat>().SetVolume);
        SFXSlider.onValueChanged.AddListener(GameManager.Instance.player.SetVolume);
        SFXSlider.onValueChanged.AddListener(GameManager.Instance.enemiesManager.SetVolume);
        SFXSlider.onValueChanged.AddListener(UpdateSFXPrefs);
        MusicSlider.onValueChanged.AddListener(GameManager.Instance.MusicPlayer.SetVolume);
    }


    void UpdateSFXPrefs(float volume)
    {
        PlayerPrefs.SetFloat("SFX", volume);
    }

}
