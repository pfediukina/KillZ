using Fusion;
using UnityEngine;

[CreateAssetMenu(menuName = "Information/Game Information")]
public class GameInfo : ScriptableObject
{
    [Range(0, 50)] public float LootTimer;
    [Range(0, 50)] public float LootItems;
}