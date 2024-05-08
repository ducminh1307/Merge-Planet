using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlanetManager))]
public class PlanetManagerUI : MonoBehaviour
{
    [Header("Elemets")]
    [SerializeField] private Image nextPlanetSprite;
    private PlanetManager planetManager;

    private void Awake()
    {
        PlanetManager.onNextPlanetSet += UpdateNextPlanetSprite;
    }

    private void OnDestroy()
    {
        PlanetManager.onNextPlanetSet -= UpdateNextPlanetSprite;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    private void UpdateNextPlanetSprite()
    {
        if (planetManager == null)
            planetManager = GetComponent<PlanetManager>();

        nextPlanetSprite.sprite = planetManager.GetNextPlanetSprite();
    }
}
