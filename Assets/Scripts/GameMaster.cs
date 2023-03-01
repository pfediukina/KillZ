using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : NetworkBehaviour
{
    [SerializeField] private StartZombie _testEnemy;
    [SerializeField] private ZombieFactory _zombieFactory;

    public static int CurrentTime { get; private set; }
    public static bool EnableTimer = false;

    private void Start()
    {
        CurrentTime = 0;
    }

    public void StartGame()
    {
        EnableTimer = true;
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        while(EnableTimer)
        { 
            yield return new WaitForSecondsRealtime(1);
            //TimeTestEvent();
            CurrentTime++;
        }
    }

    //wip
    private void TimeTestEvent()
    {
        if (CurrentTime == 5)
            _zombieFactory.StartSpawn();
        else if (CurrentTime == 10)
            _zombieFactory.EndSpawn();
    }
}
