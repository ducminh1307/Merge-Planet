using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectionOutline;
    [SerializeField] private Button unlockButton;
    [SerializeField] private Button backgroundButton;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Configure(BackgroundDataSO data)
    {
        iconImage.sprite = data.GetBackground();
        priceText.text = GameManager.instance.NumberIntToText(data.GetPrice());
    }

    public Button GetButton() => backgroundButton;

    public Button GetUnlockButton() => unlockButton;

    public void Unlock(bool active) => unlockButton.gameObject.SetActive(!active);

    public void Selected() => selectionOutline.SetActive(true);

    public void Unselect() => selectionOutline.SetActive(false);
}
