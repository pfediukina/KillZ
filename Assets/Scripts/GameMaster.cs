using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : NetworkBehaviour
{
    [SerializeField] private Enemy _testEnemy;

    public static int CurrentTime { get; private set; }
    public static bool EnableTimer = false;

    private void Start()
    {
        CurrentTime = 0;
    }

    public void StartGame()
    {
        _testEnemy.FollowNearestPlayer();
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
