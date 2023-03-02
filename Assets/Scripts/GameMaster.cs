using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMaster : NetworkBehaviour
{
    [SerializeField] private Zombie _testEnemy;
    [SerializeField] private ZombieFactory _zombieFactory;

    public static Action<int> OnTimeChanged;

    [Networked(OnChanged = nameof(TimeChanged))] public int CurrentTime { get; set; }
    private int _time;

    private static void TimeChanged(Changed<GameMaster> changed)
    {
        OnTimeChanged?.Invoke(changed.Behaviour.CurrentTime);
    }

    public static bool EnableTimer = false;

    public void StartGame()
    {
        CurrentTime = 0;
        OnTimeChanged?.Invoke(CurrentTime);
        EnableTimer = true;
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        while(EnableTimer)
        { 
            yield return new WaitForSecondsRealtime(1);
            TimeTestEvent();
            CurrentTime++;
        }
    }

    //wip
    private void TimeTestEvent()
    {
        if (CurrentTime == 5)
            _zombieFactory.StartSpawn();
        else if (CurrentTime == 40)
            _zombieFactory.EndSpawn();
    }
}
