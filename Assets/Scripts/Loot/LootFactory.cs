using Fusion;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LootFactory : NetworkBehaviour
{
    [SerializeField] private LootItem[] _items;
    private List<LootItem> _spawnedItem = new List<LootItem>();

    public void CreateLoot(LootType type)
    {
        if (HasStateAuthority)
        {
            _spawnedItem.Add(Runner.Spawn(_items[(int)type], new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0)));
            _spawnedItem.Last().transform.parent = transform;
        }
    }
}