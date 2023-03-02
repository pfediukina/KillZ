using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Information/UnitInformation")]
public class UnitInfo : ScriptableObject
{
    public float StartSpeed;
    public float AttackDelay;
    public int MaxHealth;

}