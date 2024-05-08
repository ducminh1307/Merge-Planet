using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MergeManager : MonoBehaviour
{
    [Header(" Actions ")]
    public static UnityAction<PlanetType, Vector2> onMergeProcessed;

    [Header("Settings")]
    Planet lastSender;

    void Start()
    {
        Planet.onCollisionWithFruit += CollisionBetweenFruitsCallback;
    }

    private void OnDestroy()
    {
        Planet.onCollisionWithFruit -= CollisionBetweenFruitsCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CollisionBetweenFruitsCallback(Planet sender, Planet otherPlanet)
    {
        if (lastSender != null)
            return;
        lastSender = sender;

        ProcessMerge(sender, otherPlanet);
    }

    private void ProcessMerge(Planet sender, Planet otherPlanet)
    {
        PlanetType mergePlanetType = sender.GetPlanetType();
        mergePlanetType += 1;

        Vector2 planetSpawnPos = (sender.transform.position + otherPlanet.transform.position) / 2;

        sender.Merge();
        otherPlanet.Merge();

        StartCoroutine(ResetLastSenderCoroutine());

        onMergeProcessed?.Invoke(mergePlanetType, planetSpawnPos);
    }

    IEnumerator ResetLastSenderCoroutine()
    {
        yield return new WaitForEndOfFrame();
        lastSender = null;
    }
}
