using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Information/Game Information")]
public class GameInfo : ScriptableObject
{
    [Header("Game")]
    public float RequiredPlayers;
    public float BreakTime;
    public float Waves;
    public float[] WaveTime;

    [Header("Loot")]
    public float LootTimer;
    public LootItem[] LootItems;


}
