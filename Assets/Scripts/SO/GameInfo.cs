using Fusion;
using UnityEngine;

[CreateAssetMenu(menuName = "Information/Game Information")]
public class GameInfo : ScriptableObject
{
    [Header("Waves")]
    public int WavesCount;
    public int[] WaveTimers;
    public int BreakTime;
    public int RequiredPlayers;

    [Header("Loot")]
    [Range(0, 50)] public float LootTimer;
    public LootItem[] LootItems;
}