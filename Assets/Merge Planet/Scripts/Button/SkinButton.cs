using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private GameObject selectionOutline;
    [SerializeField] private Button unlockButton;
    [SerializeField] private Button skinButton;
    [SerializeField] private TextMeshProUGUI priceText;

    public void Configure(SkinDataSO data)
    {
        iconImage.sprite = data.GetIconSkin();
        priceText.text = GameManager.instance.NumberIntToText(data.GetPrice());
    }

    public Button GetButton() => skinButton;
    public Button GetUnlockButton() => unlockButton;

    public void Unlock(bool active) => unlockButton.gameObject.SetActive(!active);

    public void Selected() => selectionOutline.SetActive(true);
    public void Unselect() => selectionOutline.SetActive(false);

}
