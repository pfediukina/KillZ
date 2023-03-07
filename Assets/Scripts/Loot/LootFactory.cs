using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootFactory : NetworkBehaviour
{
    [SerializeField] private LootItem[] _items;
    [SerializeField] private Vector2 _rangeX;
    [SerializeField] private Vector2 _rangeY;

    private float _spawnTime;
    private Coroutine _spawnTimer;

    private List<LootItem> _spawnedItem = new List<LootItem>();

    public void DespawnAll()
    {
        foreach (var item in _spawnedItem)
        {
            if (item == null) continue;
            Runner.Despawn(item.Object);
        }
        _spawnedItem.Clear();
        if(_spawnTimer != null ) StopCoroutine(_spawnTimer);
    }

    public void EnableLoot(bool enable, float time, int countOfTypes)
    {
        if (enable)
        {
            _spawnTime = time;
            if (_spawnTimer != null) StopCoroutine(_spawnTimer);
            _spawnTimer = StartCoroutine(SpawnTimer(countOfTypes));
        }
        else
            StopCoroutine(_spawnTimer);
    }

    private void CreateLoot(int countOfTypes)
    {
        if (HasStateAuthority)
        {
            for (int i = 0; i <= countOfTypes; i++)
            {
                if (i >= _items.Length) break;
                _spawnedItem.Add(Runner.Spawn(_items[i], new Vector3(Random.Range(_rangeX.x, _rangeX.y), Random.Range(_rangeY.x, _rangeY.y), 0)));
                _spawnedItem.Last().transform.parent = transform;
            }
        }
    }

    private IEnumerator SpawnTimer(int countOfTypes)
    {
        while(true)
        { 
            yield return new WaitForSeconds(_spawnTime);
            CreateLoot(countOfTypes);
        }
    }

}