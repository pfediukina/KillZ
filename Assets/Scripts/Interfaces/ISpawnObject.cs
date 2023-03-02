using Fusion;
using UnityEngine;

public interface ISpawnObject
{
    public void DespawnObject();
    public void SpawnObject(NetworkBehaviour factory, Vector3 pos);
}