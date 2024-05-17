using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI gameScoreText;
    [SerializeField] private TextMeshProUGUI menuBestScoreText;
    [SerializeField] private TextMeshProUGUI yourScoreText;

    [Header(" Settings ")]
    [SerializeField] private float scoreMultiplier;
    private int score = 0;
    private int bestScore;

    [Header("Data")]
    private const string bestScoreKey = "bestScore";

    private void Awake()
    {
        LoadData();
        MergeManager.onMergeProcessed += MergeProcessCallback;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessCallback;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    void Start()
    {
        UpdateScoreText();
        UpdateBestScoreText();
    }

    private void MergeProcessCallback(PlanetType planetType, Vector2 unused)
    {
        int scoreToAdd = (int)planetType;

        score += (int)(scoreToAdd * scoreMultiplier);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        gameScoreText.text = GameManager.instance.NumberIntToText(score);
    }

    private void UpdateBestScoreText()
    {
        menuBestScoreText.text = GameManager.instance.NumberIntToText(bestScore);
    }

    private void UpdateYourScoreText()
    {
        yourScoreText.text = GameManager.instance.NumberIntToText(score);
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Gameover:
                UpdateYourScoreText();
                CalculateBestScore();
                break;
            case GameState.Menu:
                LoadData();
                break;
        }
    }

    private void CalculateBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            SaveData();
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(bestScoreKey, bestScore);
    }

    private void LoadData()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreKey);
    }
}
