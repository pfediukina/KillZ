using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UnityEngine;

public class ZombieFactory : BaseFactory<Enemy>
{
    [SerializeField] private float spawnTime = 5;
    [SerializeField] private int startAmount = 3;
    [SerializeField] private float _radius = 10;
    [SerializeField] private float _angleOffset = 10;
    private float _currentAngle;
    private Coroutine _spawnTimer;

    private void Awake()
    {
        FactoryObjects = InitPool((int)(60 / spawnTime) + startAmount + 3, transform);
    }

    public void StartSpawn()
    {
        _spawnTimer = StartCoroutine(SpawnZombieTimer());
        SpawnInitialZombies();
    }

    public void EndSpawn()
    {
        StopCoroutine(_spawnTimer);
        foreach(var zombie in Objects)
        {
            FactoryObjects.Release(zombie);
        }
    }

    private void SpawnInitialZombies()
    {
        for (int i = 0; i < startAmount; i++)
            SpawnZombie();
    }

    private void SpawnZombie()
    {
        Enemy enemy = FactoryObjects.Get();
        enemy.transform.position = GetSpawnPosition();
        enemy.transform.parent = transform;
        Objects.Add(enemy);
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

    private IEnumerator SpawnZombieTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            
            SpawnZombie();
        }
    }
}