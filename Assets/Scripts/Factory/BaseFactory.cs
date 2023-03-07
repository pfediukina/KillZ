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

    public void SpawnObjects(int count, Vector3 startPos)
    {
        NewSpawnPos = startPos;
        SpawnObject(NewSpawnPos);
        for (int i = 1; i < count; i++)
            SpawnObject(NewSpawnPos);
    }

    public void DespawnObjects()
    {
        foreach(var Object in Objects)
        {
            Object.DespawnObject();
        }
    }

    public void DespawnObject(T obj)
    {
        FactoryObjects.Release(obj);
    }

    public void SpawnObject(Vector3 pos)
    {
        T obj = FactoryObjects.Get();
        obj.SpawnObject(this, pos);
        obj.transform.parent = transform;
        OnObjectSpawned?.Invoke();
        Objects.Add(obj);
    }

    public void StartSpawnTimer(float time)
    {
        _spawnTimer = StartCoroutine(SpawnObjectTimer(time));
    }

    public void StopSpawnTimer()
    {
        if(_spawnTimer != null )
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
