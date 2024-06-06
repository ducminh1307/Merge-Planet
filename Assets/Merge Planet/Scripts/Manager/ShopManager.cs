using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Elemets Skin")]
    [SerializeField] private GameObject itemSkinPrefab;
    [SerializeField] private RectTransform parentSkin;
    [SerializeField] private Image skinImage;
    [SerializeField] private TextMeshProUGUI skinName;

    [Header("Elemets Background")]
    [SerializeField] private GameObject backgroundPrefab;
    [SerializeField] private RectTransform parentBackground;

    [Header("Data")]
    [SerializeField] private SkinDataSO[] skinDatas;
    [SerializeField] private BackgroundDataSO[] backgroundDatas;
    private bool[] unlockSkinStates;
    private bool[] unlockBackgroundStates;

    [Header("Events")]
    public static UnityAction<SkinDataSO> onSkinSelected;
    public static UnityAction<BackgroundDataSO> onBackgroundSelected;

    public static ShopManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;

        unlockSkinStates = new bool[skinDatas.Length];
        unlockBackgroundStates = new bool[backgroundDatas.Length];
        LoadData();
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        int indexSkinSelected = PlayerPrefs.GetInt("Skin_Selected", 0);
        int indexBackgroundSelected = PlayerPrefs.GetInt("Background_Selected", 0);

        PlanetManager.instance.SkinSelectedCallback(skinDatas[indexSkinSelected]);
        ChangeBackgroundCallback(backgroundDatas[indexBackgroundSelected]);

        ChangeIconSkin(skinDatas[indexSkinSelected]);

        //Khoi tao skin
        for (int i = 0; i < skinDatas.Length;  i++)
        {
            GameObject skinButton = Instantiate(itemSkinPrefab, parentSkin);
            skinButton.GetComponent<SkinButton>().Configure(skinDatas[i]);

            int skinIndex = i;
            skinButton.GetComponent<SkinButton>().GetButton().onClick.AddListener(() => SkinButtonCallback(skinIndex));
            skinButton.GetComponent<SkinButton>().GetUnlockButton().onClick.AddListener(() => UnlockSkinButtonCallback(skinIndex));

            if (i == indexSkinSelected)
                skinButton.GetComponent<SkinButton>().Selected();

            skinButton.GetComponent<SkinButton>().Unlock(unlockSkinStates[i]);
        }

        //Khoi tao background
        for (int i = 0; i < backgroundDatas.Length; i++)
        {
            GameObject backgroundButton = Instantiate(backgroundPrefab, parentBackground);
            backgroundButton.GetComponent<BackgroundButton>().Configure(backgroundDatas[i]);

            int backgroundIndex = i;
            backgroundButton.GetComponent<BackgroundButton>().GetButton().onClick.AddListener(() => BackgroundButtonCallback(backgroundIndex));
            backgroundButton.GetComponent<BackgroundButton>().GetUnlockButton().onClick.AddListener(() => UnlockBackgroundButtonCallback(backgroundIndex));

            backgroundButton.GetComponent<BackgroundButton>().Unlock(unlockBackgroundStates[i]);

            if (i == indexBackgroundSelected)
                backgroundButton.GetComponent<BackgroundButton>().Selected();
        }
    }

    #region Background Button
    private void BackgroundButtonCallback(int backgroundIndex)
    {

        ChangeBackgroundCallback(backgroundDatas[backgroundIndex]);

        for (int i = 0; i < parentBackground.childCount; i++)
        {
            BackgroundButton backgroundButton = parentBackground.GetChild(i).GetComponent<BackgroundButton>();

            backgroundButton.Unselect();
            if (i == backgroundIndex) backgroundButton.Selected();
        }

        PlayerPrefs.SetInt("Background_Selected", backgroundIndex);

    }

    private void UnlockBackgroundButtonCallback(int backgroundIndex)
    {
        if (!CoinManager.instance.canPurchase(backgroundDatas[backgroundIndex].GetPrice()))
            return;

        unlockBackgroundStates[backgroundIndex] = true;

        parentBackground.GetChild(backgroundIndex).GetComponent<BackgroundButton>().Unlock(true);

        ChangeBackgroundCallback(backgroundDatas[backgroundIndex]);

        BackgroundButtonCallback(backgroundIndex);

        CoinManager.instance.Purchase(backgroundDatas[backgroundIndex].GetPrice());
        SaveBackgroundData();

    }
    #endregion

    #region Skin Button
    // Chuc nang doi skin
    private void SkinButtonCallback(int skinIndex)
    {
        for (int i =0; i < parentSkin.childCount; i++)
        {
            SkinButton currentButton = parentSkin.GetChild(i).GetComponent<SkinButton>();

            currentButton.Unselect();
            if (i == skinIndex) currentButton.Selected();
        }

        onSkinSelected.Invoke(skinDatas[skinIndex]);
        
        PlayerPrefs.SetInt("Skin_Selected", skinIndex);

        ChangeIconSkin(skinDatas[skinIndex]);
    }

    //Chuc nang mua skin
    private void UnlockSkinButtonCallback(int skinIndex)
    {
        // Kiem tra co the mua skin khong
        if (!CoinManager.instance.canPurchase(skinDatas[skinIndex].GetPrice()))
            return;

        // Mua skin
        unlockSkinStates[skinIndex] = true;

        parentSkin.GetChild(skinIndex).GetComponent<SkinButton>().Unlock(true);
        SkinButtonCallback(skinIndex);

        CoinManager.instance.Purchase(skinDatas[skinIndex].GetPrice());

        SaveUnlockSkinData();
    }
    #endregion

    private void ChangeIconSkin(SkinDataSO data)
    {
        skinImage.sprite = data.GetObjectPrefabs()[0].GetSprite();
        skinName.text = data.GetName();
    }

    private void ChangeBackgroundCallback(BackgroundDataSO data)
    {
        foreach (GameObject bg in GameObject.FindGameObjectsWithTag("BG"))
        {
            bg.GetComponent<Image>().sprite = data.GetBackground();
        }
    }
   
    #region Save data
    private void SaveUnlockSkinData()
    {
        for (int i = 0; i < unlockSkinStates.Length; i++)
        {
            int value = unlockSkinStates[i] ? 1 : 0;
            PlayerPrefs.SetInt($"Skin_{i}", value);
        }
    }

    private void SaveBackgroundData()
    {
        for (int i = 0; i < unlockBackgroundStates.Length; i++)
        {
            int value = unlockBackgroundStates[i] ? 1 : 0;
            PlayerPrefs.SetInt($"Background_{i}", value);
        }
    }
    #endregion

    public void LoadData()
    {
        //load skin
        for (int i = 0; i < unlockSkinStates.Length; i++)
        {
            int unlockedValue = PlayerPrefs.GetInt($"Skin_{i}", 0);

            unlockSkinStates[i] = unlockedValue == 1;
        }
        unlockSkinStates[0] = true;

        //load background
        for (int i = 0; i < unlockBackgroundStates.Length; i++)
        {
            int unlockedValue = PlayerPrefs.GetInt($"Background_{i}", 0);

            unlockBackgroundStates[i] = unlockedValue == 1;
        }
        unlockBackgroundStates[0] = true;
    }
}
