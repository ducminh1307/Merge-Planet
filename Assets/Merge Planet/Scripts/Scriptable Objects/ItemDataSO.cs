using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Scritpable Objects/Item Data", order = 2)]
public class ItemDataSO : ScriptableObject
{
    [SerializeField] private string nameItem;
    [SerializeField] private int price;
    [SerializeField] private Sprite sprite;

    public string GetNameItem() => nameItem;

    public int GetPrice() => price;

    public Sprite GetSprite() => sprite;
}
