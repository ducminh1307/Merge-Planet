using System.Collections;
using System.Collections.Generic;
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

    public void Configure(Sprite sprite) => iconImage.sprite = sprite;

    public Button GetButton() => skinButton;
    public Button GetUnlockButton() => unlockButton;

    public void Unlock(bool active) => unlockButton.gameObject.SetActive(!active);

    public void Selected() => selectionOutline.SetActive(true);
    public void Unselect() => selectionOutline.SetActive(false);
    public void SetPrice(int price) => priceText.text = GameManager.instance.NumberIntToText(price);

}
