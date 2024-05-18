using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private AudioSource mergeAudio;
    [SerializeField] private AudioSource musicAudio;

    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessCallback;
        SettingManager.onSFXValueChanged += SFXValueChangedCallback;
        SettingManager.onMusicValueChanged += MusicValueChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessCallback;
        SettingManager.onSFXValueChanged -= SFXValueChangedCallback;
        SettingManager.onMusicValueChanged -= MusicValueChangedCallback;
    }

    public void PlayMergeSound()
    {
        mergeAudio.pitch = Random.Range(.9f, 1.1f);
        mergeAudio.Play();
    }

    private void MergeProcessCallback(PlanetType arg1, Vector2 arg2)
    {
        PlayMergeSound();
    }

    private void SFXValueChangedCallback(bool sfxActive)
    {
        mergeAudio.volume = sfxActive ? 1 : 0;
    }

    private void MusicValueChangedCallback(bool musicActive)
    {
        musicAudio.Play();
        musicAudio.volume = musicActive ? 1 : 0;
    }
}
