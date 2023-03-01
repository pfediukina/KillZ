using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISpawnObject
{
    public void DespawnObject(Action OnDespawned);
    public void SpawnObject();
}