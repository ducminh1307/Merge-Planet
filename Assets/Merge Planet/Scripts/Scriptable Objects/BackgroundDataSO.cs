using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Background Data", menuName = "Scritpable Objects/Background Data", order = 1)]
public class BackgroundDataSO : ScriptableObject
{
    [SerializeField] private int price;
    [SerializeField] private Sprite background;

    public int GetPrice() => price;

    public Sprite GetBackground() => background;
}
