using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

public class ZombieFactory : BaseFactory<StartZombie>
{
    [SerializeField] private float _spawnTime = 5;
    [SerializeField] private int _startAmount = 3;
    [SerializeField] private float _radius = 10;
    [SerializeField] private float _angleOffset = 10;
    private float _currentAngle;
    private Coroutine _spawnTimer;

    private void Awake()
    {
        FactoryObjects = InitPool((int)(60 / _spawnTime) + _startAmount + 3);
        OnObjectSpawned += OnZombieSpawned;
    }

    public void StartSpawn()
    {
        StartSpawnTimer(_spawnTime);
        SpawnObjects(_startAmount, GetSpawnPosition());
    }

    public void EndSpawn()
    {
        NewSpawnPos = Vector3.zero;
        StopSpawnTimer();
        DespawnObjects();
    }

    private void OnZombieSpawned()
    {
        NewSpawnPos = GetSpawnPosition();
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 pos = Vector3.zero;
        pos.x = transform.position.x + _radius * Mathf.Cos(_currentAngle * Mathf.PI / 180);
        pos.y = transform.position.y + _radius * Mathf.Sin(_currentAngle * Mathf.PI / 180);
        _currentAngle += _angleOffset;
        _currentAngle %= 360;
        return pos;
    }
}