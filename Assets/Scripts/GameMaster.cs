using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameMaster : NetworkBehaviour
{
    [SerializeField] private EnemyFactory[] _enemyFactory;
    [SerializeField] private LootFactory _lootFactory;

    [SerializeField] private GameInfo _info;
    public static GameInfo Info { get; private set; }

    public static Action<int> OnTimeChanged;
    public static bool EnableTimer = false;

    [Networked(OnChanged = nameof(TimeChanged))] public int CurrentTime { get; set; }
    [Networked] public int CurrentWave { get; set; }

    private static int _time;
    private static Coroutine _gameTimer;
    private bool _isBreak = false;
    private Action OnTimeHasCome;

    private void Awake()
    {
        EnableTimer = true;
        Info = _info;
    }

    private static void TimeChanged(Changed<GameMaster> changed)
    {
        OnTimeChanged?.Invoke(changed.Behaviour.CurrentTime);
    }

    public void StartGame()
    {
        if (HasStateAuthority)
        {
            OnTimeChanged?.Invoke(CurrentTime);
            EnableTimer = true;

            if (_gameTimer != null)
                StopCoroutine(_gameTimer);

            _gameTimer = StartCoroutine(GameTimer());

            SpawnWave();
        }
    }

    public static void EndGame()
    {
        EnableTimer = false;
    }

    private void SpawnWave()
    {
        CurrentTime = Info.WaveTimers[CurrentWave];
        OnTimeHasCome = Break;

        for(int i = 0; i <= CurrentWave; i++)
        {
            //_enemyFactory[i].SpawnObjects(10,Vector3.zero);
            _enemyFactory[i].StartSpawn();
        }
        _lootFactory.DespawnAll();

        _lootFactory.EnableLoot(true, Info.LootTimer, CurrentWave);
    }

    private void Break()
    {
        CurrentTime = Info.BreakTime;
        OnTimeHasCome = SpawnWave;

        for (int i = 0; i <= CurrentWave; i++)
        {
            if (i >= _enemyFactory.Length) break;
            _enemyFactory[i].DespawnObjects();
            _enemyFactory[i].StopSpawnTimer();
        }

        CurrentWave++;
        if (CurrentWave == Info.WavesCount)
        {
            EndGame();
            return;
        }
    }

    private IEnumerator GameTimer()
    {
        while (EnableTimer)
        {
            yield return new WaitForSecondsRealtime(1);
            //TimeTestEvent();
            CurrentTime--;
            if(CurrentTime <= 0)
            {
                OnTimeHasCome?.Invoke();
            }
        }
    }
}
