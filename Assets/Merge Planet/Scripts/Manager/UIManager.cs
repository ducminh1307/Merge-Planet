using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                SetMenu();
                break;

            case GameState.Game:
                SetGame();
                break;


            case GameState.Gameover:
                SetGameover();
                break;
        }
    }

    private void SetMenu()
    {
        UIPanel.SetActive(true);
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        shopPanel.SetActive(false);
    }

    private void SetGame()
    {
        UIPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    private void SetGameover()
    {
        UIPanel.SetActive(true);
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(true);
    }

    public void PlayButtonCallback()
    {
        GameManager.instance.SetGameState();
        SetGame();
    }

    public void NextButtonCallback()
    {
        SceneManager.LoadScene(0);
    }

    public void SettingButtonCallback()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettingPanel ()
    {
        settingsPanel.SetActive(false);
    }

    public void ShopButtonCallback()
    {
        shopPanel.SetActive(true);
        TabUI.instance.switchToTab(0);
        menuPanel.SetActive(false);
        ShopManager.instance.LoadData();
    }

    public void BackButtonCallback()
    {
        shopPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
