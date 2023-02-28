using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : NetworkBehaviour
{
    [SerializeField] private Enemy _testEnemy;
    [SerializeField] private ZombieFactory _zombieFactory;

    public static int CurrentTime { get; private set; }
    public static bool EnableTimer = false;

    private void Start()
    {
        CurrentTime = 0;
    }

    private void Update()
    {
        if(CurrentTime == 60)
            _zombieFactory.EndSpawn();
    }

    public void StartGame()
    {
        _zombieFactory.StartSpawn();
        EnableTimer = true;
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        while(EnableTimer)
        { 
            yield return new WaitForSecondsRealtime(1);
            CurrentTime++;
        }
    }
}
