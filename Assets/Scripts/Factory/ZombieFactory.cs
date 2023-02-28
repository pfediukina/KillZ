using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ZombieFactory : BaseFactory<Enemy>
{
    [SerializeField] private float spawnTime = 5;
    [SerializeField] private int startAmount = 3;

    private void Awake()
    {
        //FactoryObjects = InitPool((int)(60 / spawnTime) + startAmount + 3, transform);
    }

    public void StartSpawn()
    {
        //StartCoroutine(SpawnZombieTimer());
        SpawnInitialZombies();
    }

    public void EndSpawn()
    {
        //StopCoroutine(SpawnZombieTimer());
    }

    private void SpawnInitialZombies()
    {
        for (int i = 0; i < startAmount; i++)
            SpawnZombie();
    }

    private void SpawnZombie()
    {
        Enemy enemy = Runner.Spawn(Prefab, transform.position);
        enemy.transform.parent = transform;
    }

    private IEnumerator SpawnZombieTimer()
    {
        yield return new WaitForSeconds(spawnTime);
        SpawnZombie();
    }
}