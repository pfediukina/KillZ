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
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _enemyLayer;
    [HideInInspector] public bool StartMove = false;
    
    private Vector3 _dir;
    private Vector3 _endPoint;
    private string _tag;
    private bool _explose;

    public Unit From { get; set; }
    public int Damage { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!HasStateAuthority) return;
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
            transform.position += _dir * Runner.DeltaTime * _speed;
        if(Vector3.Distance(transform.position, _endPoint) <= 0.2)
        {
            Runner.Despawn(Object);
        }
    }

    public void InitBullet(Unit owner, int damage, Vector3 moveTo, string Tag, float distance, bool explosive = false)
    { 
        MoveTo(moveTo, distance);
        From = owner;
        Damage = damage;
        _tag = Tag;
        _explose = explosive;
    }

    private void MoveTo(Vector3 pos, float distance)
    {
        //Debug.Log(pos);
        _dir = pos;
        _endPoint = pos * distance + transform.position;
        StartMove = true;
    }

    public void GiveDamage(Unit from, Unit to, int damage)
    {
        if (_explose && from is Player)
        {
            Debug.Log("Here");
            RPC_Explose(from as Player);
            return;
        }
        if (to.IsDead) return;
        to.TakeDamage(from, damage);
        Runner.Despawn(Object);
    }

    [Rpc]
    public void RPC_Explose(Player player)
    {
        Debug.Log("Here");
        var colliders = Physics2D.OverlapCircleAll(transform.position, 2f, _enemyLayer);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<Enemy>(out var enemy))
            {
                GiveDamage(player, enemy, Damage);
            }
        }

    }
}