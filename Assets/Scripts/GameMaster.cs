using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameMaster : NetworkBehaviour
{
    [SerializeField] private ZombieFactory _zombieFactory;
    [SerializeField] private LootFactory _lootFactory;

    [SerializeField] private GameInfo _info;
    public GameInfo Info => _info;


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
        while (EnableTimer)
        {
            yield return new WaitForSecondsRealtime(1);
            //TimeTestEvent();
            CurrentTime++;

            if(CurrentTime % Info.LootTimer == 0)
            {
                _lootFactory.CreateLoot(LootType.Heal);
                _lootFactory.CreateLoot(LootType.Ammo);
                _lootFactory.CreateLoot(LootType.Bomb);
                _zombieFactory.SpawnObjects(3, Vector3.zero);
            }
        }
    }
}
