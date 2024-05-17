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
        for (int i = 0; i < skinDatas.Length;  i++)
        {
            GameObject skinButtonInstance = Instantiate(itemSkinPrefab, parentSkin);
            skinButtonInstance.GetComponent<SkinButton>().Configure(skinDatas[i].GetIconSkin());

            int j = i;
            skinButtonInstance.GetComponent<SkinButton>().GetButton().onClick.AddListener(() => SkinButtonCallback(j));

            if (i == 0)
                skinButtonInstance.GetComponent<SkinButton>().Selected();

            skinButtonInstance.GetComponent<SkinButton>().Active(unlockStates[i]);

        }
    }

    private void SkinButtonCallback(int id)
    {
        for (int i =0; i < parentSkin.childCount; i++)
        {
            SkinButton currentButton = parentSkin.GetChild(i).GetComponent<SkinButton>();

            if (i == id)
                currentButton.Selected();
            else
                currentButton.Unselect();
        }

        if (IsSkinActive(id))
            onSkinSelected.Invoke(skinDatas[id]);
    }

    private bool IsSkinActive(int id) => unlockStates[id];

    private void LoadData()
    {
        unlockStates[0] = true;

        if (unlockStates.Length > 2)
        {
            for (int i = 1; i < unlockStates.Length; i++)
            {
                int unlockedValue = PlayerPrefs.GetInt($"Skin_{i}");

                if (unlockedValue == 1) unlockStates[i] = true;
            }
        }
    }
}
