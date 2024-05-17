using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopManager : MonoBehaviour
{
    [Header("Elemets")]
    [SerializeField] private GameObject itemSkinPrefab;
    [SerializeField] private RectTransform parentSkin;

    [Header("Data")]
    [SerializeField] private SkinDataSO[] skinDatas;
    private bool[] unlockStates;

    [Header("Events")]
    public static UnityAction<SkinDataSO> onSkinSelected;

    private void Awake()
    {
        unlockStates = new bool[skinDatas.Length];
        LoadData();
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        int indexSkinSelected = PlayerPrefs.GetInt("SkinSelected");
        for (int i = 0; i < skinDatas.Length;  i++)
        {
            GameObject skinButtonInstance = Instantiate(itemSkinPrefab, parentSkin);
            skinButtonInstance.GetComponent<SkinButton>().Configure(skinDatas[i].GetIconSkin());

            int skinIndex = i;
            skinButtonInstance.GetComponent<SkinButton>().GetButton().onClick.AddListener(() => SkinButtonCallback(skinIndex));
            skinButtonInstance.GetComponent<SkinButton>().GetUnlockButton().onClick.AddListener(() => UnlockButtonCallback(skinIndex));
            skinButtonInstance.GetComponent<SkinButton>().SetPrice(skinDatas[i].GetPrice());

            if (i == indexSkinSelected)
                skinButtonInstance.GetComponent<SkinButton>().Selected();

            skinButtonInstance.GetComponent<SkinButton>().Unlock(unlockStates[i]);
        }
    }

    private void SkinButtonCallback(int skinIndex)
    {
        for (int i =0; i < parentSkin.childCount; i++)
        {
            SkinButton currentButton = parentSkin.GetChild(i).GetComponent<SkinButton>();

            if (i == skinIndex)
                currentButton.Selected();
            else
                currentButton.Unselect();
        }

        if (IsSkinActive(skinIndex))
            onSkinSelected.Invoke(skinDatas[skinIndex]);

        PlayerPrefs.SetInt("SkinSelected", skinIndex);
    }

    private void UnlockButtonCallback(int skinIndex)
    {
        // Check current coin can purchase the skin
        if (!CoinManager.instance.canPurchase(skinDatas[skinIndex].GetPrice()))
            return;

        //Purchase the skin
        unlockStates[skinIndex] = true;

        parentSkin.GetChild(skinIndex).GetComponent<SkinButton>().Unlock(true);
        SkinButtonCallback(skinIndex);

        SaveData();
    }

    private bool IsSkinActive(int id) => unlockStates[id];

    private void SaveData()
    {
        for (int i = 0; i < unlockStates.Length; i++)
        {
            int value = unlockStates[i] ? 1 : 0;
            PlayerPrefs.SetInt($"Skin_{i}", value);
        }
    }

    private void LoadData()
    {
        for (int i = 1; i < unlockStates.Length; i++)
        {
            int unlockedValue = PlayerPrefs.GetInt($"Skin_{i}");

            if (unlockedValue == 1) unlockStates[i] = true;
        }
        unlockStates[0] = true;
    }
}
