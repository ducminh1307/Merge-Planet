using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private AudioSource mergeAudio;

    private void Awake()
    {
        MergeManager.onMergeProcessed += MergeProcessCallback;
        SettingManager.onSFXValueChanged += SFXValueChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessCallback;
        SettingManager.onSFXValueChanged -= SFXValueChangedCallback;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
