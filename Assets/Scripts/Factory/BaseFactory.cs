using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseFactory<T> : NetworkBehaviour where T : NetworkBehaviour, ISpawnObject
{
    [SerializeField] protected T Prefab;
    //[SerializeField] protected NetworkRunner runner;

    protected ObjectPool<T> FactoryObjects;
    protected HashSet<T> Objects = new HashSet<T>();
    protected Vector3 NewSpawnPos = Vector3.zero;
    protected Action OnObjectSpawned;
    protected Action<ObjectPool<T>> OnObjectDespawned;

    private Coroutine _spawnTimer;

    protected ObjectPool<T> InitPool(int count)
    {
        ObjectPool<T> list = new ObjectPool<T>(createFunc: () =>
            Runner.Spawn(Prefab, transform.position),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            maxSize: count);

        return list;
    }

    protected void SpawnObjects(int count, Vector3 startPos)
    {
        NewSpawnPos = startPos;
        SpawnObject(NewSpawnPos);
        for (int i = 1; i < count; i++)
            SpawnObject(NewSpawnPos);
    }

    protected void DespawnObjects()
    {
        foreach(var Object in Objects)
        {
            Object.DespawnObject( () => { FactoryObjects.Release(Object); });
        }
    }

    protected void SpawnObject(Vector3 pos)
    {
        T obj = FactoryObjects.Get();
        obj.transform.position = pos;
        obj.transform.parent = transform;
        obj.SpawnObject();
        Objects.Add(obj);
        OnObjectSpawned?.Invoke();
    }

    protected void StartSpawnTimer(float time)
    {
        _spawnTimer = StartCoroutine(SpawnObjectTimer(time));
    }

    protected void StopSpawnTimer()
    {
        StopCoroutine(_spawnTimer);
    }

    private IEnumerator SpawnObjectTimer(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            SpawnObject(NewSpawnPos);
        }
    }
}
