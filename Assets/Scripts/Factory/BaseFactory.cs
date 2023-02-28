using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseFactory<T> : NetworkBehaviour where T : NetworkBehaviour
{
    [SerializeField] protected T Prefab;
    //[SerializeField] protected NetworkRunner runner;

    protected ObjectPool<T> FactoryObjects;

    protected ObjectPool<T> InitPool(int count, Transform parent)
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
}
