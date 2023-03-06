using Fusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    public float Speed { private get; set; }
    private Vector3 _movePos;

    [HideInInspector] public bool StartMove = false;
    public Unit Owner { get; set; }

    private void Awake()
    {
        tag = "Bullet";
        Speed = 0;
    }

    private void Update()
    {
        if(StartMove)
            transform.position = Vector3.MoveTowards(transform.position, _movePos, Runner.DeltaTime * Speed);
        if (Vector3.Distance(transform.position, _movePos) < 0.01f)
            Runner.Despawn(Object);
    }

    public void MoveTo(Vector3 pos)
    {
        _movePos = pos;
        StartMove = true;
    }
}