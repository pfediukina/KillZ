using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Information/Unit Information")]
public class UnitInfo : ScriptableObject
{
    public float StartSpeed;
    public float AttackDelay;
    public int MaxHealth;
    public int Score;
}