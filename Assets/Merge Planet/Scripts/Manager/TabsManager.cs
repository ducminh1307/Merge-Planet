using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabsManager : MonoBehaviour
{
    [Header("Element of tab button")]
    [SerializeField] private Image[] tabButtons;
    [SerializeField] private TextMeshProUGUI[] tabButtonTexts;

    [Header("Element of tab content")]
    [SerializeField] private GameObject[] tabs;

    [Header("Setting")]
    [SerializeField] private Color inactiveButtonColor;
    [SerializeField] private Vector2 inactiveButtonSize;
    [SerializeField] private Vector2 inactiveTextSize;
    [SerializeField] private Color inactiveTextColor;

    [Space][Space]

    [SerializeField] private Color activeButtonColor;
    [SerializeField] private Vector2 activeButtonSize;
    [SerializeField] private Vector2 activeTextSize;
    [SerializeField] private Color activeTextColor;

    public void switchToTab(int tabId)
    {
        foreach (Image tabButton in tabButtons)
        {
            tabButton.color = inactiveButtonColor;
            tabButton.rectTransform.sizeDelta = inactiveButtonSize;
        }

        foreach (TextMeshProUGUI tabText in tabButtonTexts)
        {
            tabText.color = inactiveTextColor;
            tabText.rectTransform.sizeDelta = inactiveTextSize;
        }

        foreach (GameObject tab in tabs)
        {
            tab.SetActive(false);
        }

        tabButtons[tabId].color = activeButtonColor;
        tabButtons[tabId].rectTransform.sizeDelta = activeButtonSize;

        tabButtonTexts[tabId].rectTransform.sizeDelta = activeTextSize;
        tabButtonTexts[tabId].color = activeTextColor;

        tabs[tabId].SetActive(true);
    }

    private void Start()
    {
        switchToTab(0);
    }
}
