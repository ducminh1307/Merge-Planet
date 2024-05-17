using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject resetProgessPrompt;
    [SerializeField] private Toggle sfxToggle;

    [Header(" Actions ")]
    public static UnityAction<bool> onSFXValueChanged;

    [Header(" Datas ")]
    private const string sfxActiveKey = "sfxActive";

    private void Awake()
    {
        LoadData();
    }

    void Start()
    {
        //SliderValueChangedCallback();
        //ToggleCallback();
    }

    public void ResetProgessButtonCallback()
    {
        resetProgessPrompt.SetActive(true);
    }

    public void ResetProgessYes()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    public void ResetProgessNo()
    {
        resetProgessPrompt.SetActive(false);
    }

    public void ToggleCallback()
    {
        onSFXValueChanged?.Invoke(sfxToggle.isOn);
        SaveData();
    }

    private void LoadData()
    {
        if (!PlayerPrefs.HasKey(sfxActiveKey))
            PlayerPrefs.SetInt(sfxActiveKey, 1);

        sfxToggle.isOn = PlayerPrefs.GetInt(sfxActiveKey) == 1;
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(sfxActiveKey, sfxToggle.isOn ? 1 : 0);
    }
}
