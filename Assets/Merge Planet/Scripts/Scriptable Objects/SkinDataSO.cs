using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skin Data", menuName = "Scritpable Objects/Skin Data", order = 0)]
public class SkinDataSO : ScriptableObject
{
    [SerializeField] private int price;

    [SerializeField] private Sprite iconSkin;

    [SerializeField] private Planet[] objectPrefabs;

    [SerializeField] private Planet[] spawnablePrefabs;


    public Planet[] GetObjectPrefabs() => objectPrefabs;

    public Planet[] GetSpawnablePrefabs() => spawnablePrefabs;

    public Sprite GetIconSkin() => iconSkin;

    public int GetPrice() => price;
}
