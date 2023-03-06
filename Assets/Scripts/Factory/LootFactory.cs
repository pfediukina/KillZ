using Fusion;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LootFactory : NetworkBehaviour
{
    [SerializeField] private Vector2 _rangeX;
    [SerializeField] private Vector2 _rangeY;
    [SerializeField] private Rect _rect;
    [SerializeField] private Transform _parent;
    
    private List<LootItem> _spawned = new List<LootItem>();

    public void SpawnObject(int typeCount)
    {
        Debug.Log("Spawn loot");
        for (int i = 0; i < typeCount + 1; i++)
        {

            float randomX = Random.Range(_rangeX.x, _rangeX.y);
            float randomY = Random.Range(_rangeY.x, _rangeY.y);
            var loot = Runner.Spawn(GameManager.Info.LootItems[i], new Vector3(randomX, randomY, 0));
            loot.transform.parent = _parent;

            _spawned.Add(loot);
        }
    }

    public void DespawnAll()
    {
        foreach (var item in _spawned)
        {
            if (item == null) continue;
            Runner.Despawn(item.Object); 
        }
        _spawned.Clear();
    }
}