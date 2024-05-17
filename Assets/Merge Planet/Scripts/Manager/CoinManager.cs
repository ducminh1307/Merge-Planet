using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Variables")]
    private int coins;
    private const string coinKey = "Coins";

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI coinTextHome;
    [SerializeField] private TextMeshProUGUI coinTextShop;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        LoadData();
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void AddCoin(int _coins)
    {
        coins += _coins;
        SaveData();

        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinTextHome.text = GameManager.instance.NumberIntToText(coins);
        coinTextShop.text = GameManager.instance.NumberIntToText(coins);
    }

    public bool canPurchase(int price) => coins >= price;

    private void LoadData() => coins = PlayerPrefs.GetInt(coinKey);

    private void SaveData() => PlayerPrefs.SetInt(coinKey, coins);
}
