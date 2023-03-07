using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

public class Bullet : NetworkBehaviour, IDamaging
{
    [SerializeField] private float _speed = 4f;
    [HideInInspector] public bool StartMove = false;
    
    private Vector3 _dir;
    private string _tag;

    public Unit From { get; set; }
    public int Damage { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == _tag)
        {
            if(collision.TryGetComponent<Unit>(out var unit))
            {
                GiveDamage(From, unit, Damage);
            }
        }
    }

    private void Update()
    {
        if (StartMove)
            transform.position += _dir * Runner.DeltaTime;
        if(Vector3.Distance(transform.position, _dir) <= 0)
        {
            Runner.Despawn(Object);
        }
    }

    public void InitBullet(Unit owner, int damage, Vector3 moveTo, string Tag)
    {
        MoveTo(moveTo);
        From = owner;
        Damage = damage;
        _tag = Tag;
    }

    private void MoveTo(Vector3 pos)
    {
        _dir = pos;
        StartMove = true;
    }

    public void GiveDamage(Unit from, Unit to, int damage)
    {
        if (to.IsDead) return;
        to.TakeDamage(from, damage);
        Runner.Despawn(Object);
    }
}