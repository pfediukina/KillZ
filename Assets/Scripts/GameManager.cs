using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : NetworkBehaviour
{
    [SerializeField] private ZombieFactory[] _zombieFactories;
    [SerializeField] private LootFactory _lootFactory;

    [SerializeField] private GameInfo _gameInfo;

    [Networked(OnChanged = nameof(TimeChanged))] public int CurrentTime { get; set; }
    [Networked] public int CurrentWave { get; set; }

    public static Action<int> OnTimeChanged;
    public static bool EnableTimer = false;
    public static GameInfo Info { get; private set; }

    private bool _isBreak = false;

    private void Awake()
    {
        Info = _gameInfo;
    }

    private static void TimeChanged(Changed<GameManager> changed)
    {
        OnTimeChanged?.Invoke(changed.Behaviour.CurrentTime);
    }

    //start when 2 players
    public void StartGame()
    {
        OnTimeChanged?.Invoke(CurrentTime);
        ShowAllPlayerGameText(GameText.Intro);
        //SpawnWave();
        EnableTimer = true;
        StartCoroutine(GameTimer());
    }

    public void EndGame()
    {
        EnableTimer = false;
        Break();
        ShowAllPlayerGameText(GameText.Intro);
        Launcher.DisconnectAll();
    }

    private void ShowAllPlayerGameText(GameText type)
    {
        foreach (var transform in Launcher.Chars)
        {
            if (transform.TryGetComponent<Player>(out var player))
            {
                if (type == GameText.Survived)
                {
                    if (player.States.CurrentState is DeadState)
                        player.UI.GameText.ShowGameText(GameText.Dead);
                    else
                        player.UI.GameText.ShowGameText(GameText.Survived);
                }
                else
                    player.UI.GameText.ShowGameText(type);
            }
        }
    }

    private void SpawnWave()
    {
        CurrentTime = 0;
        _isBreak = false;

        _lootFactory.DespawnAll();
        for (int i = 0; i <= CurrentWave; i++) 
        {
            _zombieFactories[i].StartSpawn();
        }
    }

    private void Break()
    {
        _isBreak = true;
        ShowAllPlayerGameText(GameText.Survived);
        for (int i = 0; i <= CurrentWave; i++)
        {
            if (i == Info.Waves) continue;
            _zombieFactories[i].EndSpawn();
        }

        CurrentTime = 0;
        //playerUI SURVIVED
        StartCoroutine(BreakTimer());
        CurrentWave++;
    }

    private IEnumerator GameTimer()
    {
        while(EnableTimer)
        { 
            yield return new WaitForSecondsRealtime(1);
            CurrentTime++;
            if (CurrentWave == Info.Waves)
            {
                EndGame();
                yield return null;
            }

            if (!_isBreak && CurrentTime >= _gameInfo.WaveTime[CurrentWave])
            {
                Break();
            }

            if (CurrentTime % _gameInfo.LootTimer == 0 && !_isBreak)
            {
                _lootFactory.SpawnObject(CurrentWave);
            }
        }
    }

    private IEnumerator BreakTimer()
    {
        yield return new WaitForSeconds(_gameInfo.BreakTime);
        if(CurrentWave != Info.Waves)
            SpawnWave();
    }
}
