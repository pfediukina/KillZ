using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseWeapon : NetworkBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    public SpriteRenderer GetSprite() => _sprite;
}
