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
        gameScoreText.text = score.ToString("0000");
    }

    private void UpdateBestScoreText()
    {
        menuBestScoreText.text = bestScore.ToString("0000");
    }

    private void UpdateYourScoreText()
    {
        yourScoreText.text = score.ToString("0000");
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Gameover:
                UpdateYourScoreText();
                CalculateCoinToEarn();
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

    private void CalculateCoinToEarn()
    {
        double result = score / CoinManager.instance.coinCoefficient;
        int coin = (int)Math.Round(result);
        CoinManager.instance.UpdateCointEarnText(coin);
        CoinManager.instance.AddCoin(coin);
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
