using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms.Impl;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("Variables")]
    private int coins;
    private const string coinKey = "Coins";
    [field:SerializeField] public int coinCoefficient { get; private set; }

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI coinHomeText;
    [SerializeField] private TextMeshProUGUI coinShopText;
    [SerializeField] private TextMeshProUGUI coinEarnText;

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

    public void Purchase(int price)
    {
        coins -= price;
        SaveData();

        UpdateCoinText();
    }

    public void UpdateCoinText()
    {
        coinHomeText.text = GameManager.instance.NumberIntToText(coins);
        coinShopText.text = GameManager.instance.NumberIntToText(coins);
    }

    public void UpdateCointEarnText(int coin)
    {
        coinEarnText.text = GameManager.instance.NumberIntToText(coin);
    }

    public bool canPurchase(int price) => coins >= price;

    private void LoadData() => coins = PlayerPrefs.GetInt(coinKey);

    private void SaveData() => PlayerPrefs.SetInt(coinKey, coins);
}
