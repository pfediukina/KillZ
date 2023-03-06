using Fusion;
using UnityEngine;

[CreateAssetMenu(menuName = "Information/Weapon Information")]
public class WeaponInfo : ScriptableObject
{
    [Range(0, 15)] public float AttackDistance;
    public float AttackRate;
    public int Damage;
    public int MaxAmmo;
    public bool IsExplosive;
}