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
        onSFXValueChanged?.Invoke(sfxToggle.isOn);
        SaveData();
    }
    
    public void MusicToggleCallback()
    {
        onMusicValueChanged?.Invoke(musicToggle.isOn);
        SaveData();
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

    private void SaveData()
    {
        PlayerPrefs.SetInt(sfxActiveKey, sfxToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt(musicActiveKey, musicToggle.isOn ? 1 : 0);
    }
}
