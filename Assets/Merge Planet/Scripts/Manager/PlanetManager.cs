using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class PlanetManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SkinDataSO skinData;
    [SerializeField] private Transform planetsParent;
    [SerializeField] private LineRenderer planetSpawnLine;
    private Planet currentPlanet;
    
    [Header(" Settings ")]
    [SerializeField] private float planetYSpawnPos;
    [SerializeField] private float spawnDelay;
    private bool canControl;
    private bool isControlling;
    
    [Header(" Next Planet Settings ")]
    private int nextPlanetIndex;

    [Header(" Debug ")]
    [SerializeField] private bool enableGizmos;

    [Header(" Actions ")]
    [SerializeField] public static Action onNextPlanetSet;

    public static PlanetManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);

        MergeManager.onMergeProcessed += MergeProcessedCalback;
        ShopManager.onSkinSelected += SkinSelectedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onMergeProcessed -= MergeProcessedCalback;
        ShopManager.onSkinSelected -= SkinSelectedCallback;
    }

    void Start()
    {
        canControl = true;
        HideLine();
    }

    void Update()
    {
        if (!GameManager.instance.IsGameState())
            return;

        if (GetClickedWorldPosition().y < planetYSpawnPos)
            if (canControl)
                ManagePlayerInput();  
    }

    private void ManagePlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
            MouseDownCallback();

        else if (Input.GetMouseButton(0) && isControlling)
        {
            if(isControlling)
                MouseDragCallback();
            else
                MouseDownCallback();
        }

        else if (Input.GetMouseButtonUp(0) && isControlling)
            MouseUpCallback();
    }

    private void MouseDownCallback()
    {
        DisplayLine();
        PlaceLineAtClickedPosition();

        SpawnPlanet();

        isControlling = true;
    }

    private void MouseDragCallback()
    {
        PlaceLineAtClickedPosition();

        currentPlanet.MoveTo(new Vector2(GetSpawnPosition().x, planetYSpawnPos));
    }

    private void MouseUpCallback()
    {
        HideLine();

        if( currentPlanet != null )
            currentPlanet.EnablePhysics();

        canControl = false;
        StartControlTimer();

        isControlling = false;

        SetNextPlanetIndex();
    }

    private void SpawnPlanet()
    {
        Vector2 spawnPosition = GetSpawnPosition();
        Planet planetToInstatiate = skinData.GetSpawnablePrefabs()[nextPlanetIndex];

        currentPlanet = Instantiate(
            planetToInstatiate, 
            spawnPosition, 
            Quaternion.identity,
            planetsParent
            );
    }

    private void SetNextPlanetIndex()
    {
        nextPlanetIndex = Random.Range(0, skinData.GetSpawnablePrefabs().Length);
        onNextPlanetSet?.Invoke();
    }

    public string GetNextPlanetName()
    {
        return skinData.GetSpawnablePrefabs()[nextPlanetIndex].GetPlanetType().ToString();
    }

    public Sprite GetNextPlanetSprite()
    {
        return skinData.GetSpawnablePrefabs()[nextPlanetIndex].GetSprite();
    }

    private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 worldClickedPosition = GetClickedWorldPosition();
        worldClickedPosition.y = planetYSpawnPos;
        return worldClickedPosition;
    }

    private void PlaceLineAtClickedPosition()
    {
        planetSpawnLine.SetPosition(0, GetSpawnPosition());
        planetSpawnLine.SetPosition(1, GetSpawnPosition() + Vector2.down * 15);
    }

    private void HideLine()
    {
        planetSpawnLine.enabled = false;
    }

    private void DisplayLine()
    {
        planetSpawnLine.enabled = true;
    }

    private void StartControlTimer()
    {
        Invoke("StopControlTimer", spawnDelay);
    }

    private void StopControlTimer()
    {
        canControl = true;
    }

    private void MergeProcessedCalback(PlanetType planetType, Vector2 spawnPosition)
    {
        for(int i = 0; i < skinData.GetObjectPrefabs().Length; i++)
        {
            if (skinData.GetObjectPrefabs()[i].GetPlanetType() == planetType)
            {
                SpawnMergePlanet(skinData.GetObjectPrefabs()[i], spawnPosition);
                break;
            }
        }
    }

    private void SpawnMergePlanet(Planet planet, Vector2 spawnPosition)
    {
        Planet planetInstantiate = Instantiate(planet, spawnPosition, Quaternion.identity, planetsParent);
        planetInstantiate.EnablePhysics();
    }

    public void SkinSelectedCallback(SkinDataSO skin)
    {
        skinData = skin;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!enableGizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(-50, planetYSpawnPos, 0), new Vector3(50, planetYSpawnPos, 0));
    }
#endif

}
