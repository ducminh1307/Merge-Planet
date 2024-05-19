using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle musicToggle;

    [Header(" Actions ")]
    public static UnityAction<bool> onSFXValueChanged;
    public static UnityAction<bool> onMusicValueChanged;

    [Header(" Datas ")]
    private const string sfxActiveKey = "sfxActive";
    private const string musicActiveKey = "musicActive";

    private void Awake()
    {
        LoadData();
    }

    public void SFXToggleCallback()
    {
        SaveSFX();
        onSFXValueChanged?.Invoke(sfxToggle.isOn);
    }

    public void MusicToggleCallback()
    {
        SaveMusic();
        onMusicValueChanged?.Invoke(musicToggle.isOn);
    }

    private void LoadData()
    {
        if (!PlayerPrefs.HasKey(sfxActiveKey))
            PlayerPrefs.SetInt(sfxActiveKey, 1);

        if (!PlayerPrefs.HasKey(musicActiveKey))
            PlayerPrefs.SetInt(musicActiveKey, 1);

        sfxToggle.isOn = PlayerPrefs.GetInt(sfxActiveKey) == 1;
        musicToggle.isOn = PlayerPrefs.GetInt(musicActiveKey) == 1;
    }

    private void SaveSFX() => PlayerPrefs.SetInt(sfxActiveKey, sfxToggle.isOn ? 1 : 0);

    private void SaveMusic() => PlayerPrefs.SetInt(musicActiveKey, musicToggle.isOn ? 1 : 0);
    
}
