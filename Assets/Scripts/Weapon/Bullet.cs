using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _speed = 4f;
    [HideInInspector] public bool StartMove = false;
    
    private Vector3 _dir;

    public Unit Owner { get; set; }
    private int Damage { get; set; }

    private void Update()
    {
        if (StartMove)
            transform.position += _dir * Runner.DeltaTime;
        if(Vector3.Distance(transform.position, _dir) <= 0)
        {
            Runner.Despawn(Object);
        }
    }

    public void InitBullet(Unit owner, int damage, Vector3 moveTo)
    {
        MoveTo(moveTo);
        Owner = owner;
        Damage = damage;
    }

    private void MoveTo(Vector3 pos)
    {
        _dir = pos;
        StartMove = true;
    }
}