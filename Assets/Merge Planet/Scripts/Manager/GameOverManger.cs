using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManger : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject deadline;
    [SerializeField] private Transform planetsParents;

    [Header(" Timer ")]
    [SerializeField] private float duartionThreshold;
    private float timer;
    private bool timerOn;
    private bool isGameover;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameover)
            ManageGameover();
    }

    private void ManageGameover()
    {
        if (timerOn)
        {
            ManageTimerOn();
        }
        else
        {
            if (IsPlanetAboveLine())
                StartTimer();
        }
    }

    private void ManageTimerOn()
    {
        timer += Time.deltaTime;

        if(!IsPlanetAboveLine())
            StopTimer();

        if( timer >= duartionThreshold)
        {
            GameOver();
        }
    }

    private bool IsPlanetAboveLine()
    {
        for (int i = 0; i < planetsParents.childCount; i++)
        {
            Planet planet = planetsParents.GetChild(i).GetComponent<Planet>();
            if (!planet.HasCollided())
                continue;
            if(IsPlanetAboveLine(planetsParents.GetChild(i)))
                return true;
        }
        return false;
    }

    private bool IsPlanetAboveLine(Transform planet)
    {
        if (planet.position.y > deadline.transform.position.y)
            return true;

        return false;
    }

    private void StartTimer()
    {
        timer = 0;
        timerOn = true;
    }

    private void StopTimer()
    {
        timerOn = false;
    }

    private void GameOver()
    {
        GameManager.instance.SetGameoverState();
        isGameover = true;
    }
}
