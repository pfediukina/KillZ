using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _speed = 4f;
    private Vector3 _movePos;

    [HideInInspector] public bool StartMove = false;
    
    private void Update()
    {
        if(StartMove)
            transform.position = Vector3.MoveTowards(transform.position, _movePos, Runner.DeltaTime * _speed);
        if(Vector3.Distance(transform.position, _movePos) <= 0.1)
        {
            Runner.Despawn(Object);
        }
    }

    public void MoveTo(Vector3 pos)
    {
        _movePos = pos;
        StartMove = true;
    }
}