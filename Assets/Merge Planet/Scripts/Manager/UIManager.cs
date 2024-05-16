using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    private void Awake()
    {
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    private void SetGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameoverPanel.SetActive(false);
    }
    private void SetGameover()
    {
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
    }

    public void BackButtonCallback()
    {
        shopPanel.SetActive(false);
    }
}
